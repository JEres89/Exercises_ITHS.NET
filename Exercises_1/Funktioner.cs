using Exercises_1;
using static CommonFunctionality.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Funktioner : Exercises
{
	private static Funktioner? funktioner;
	private Funktioner()
	{
		string name = "Funktioner";
		Dictionary<int, Action> exercises = new()
		{
			{ 1, Funktioner1 },
			{ 2, Funktioner2 },
			{ 3, Funktioner3 },
			{ 4, Funktioner4 }
		};
		Init(name, exercises);
	}

	public static Funktioner GetFunktioner()
	{
		return ( funktioner ??= new Funktioner() );
	}

	public static void Funktioner1()
	{
        Console.WriteLine("Miniuppgift 1.1 & 1.2");
		string name = GetTextInput("namn");
		int num;
		Console.Write("Mata in antal: ");
		GetInt(out num, 0);
		Greet(name, num);

		static void Greet(string name, int num = 1)
		{
			for(int i = 0 ; i < num ; i++)
			{
				Console.WriteLine($"Hej {name}!");
			}
		}
		PromptContinue();

		/* Slå ihop för- och efternamn - skriv ut
		Skriv en funktion som tar två parametrar: firstName och lastName. Funktionen ska skriva ut hela namnet på skärmen. Testa genom att anropa funktionen med ditt namn.

		Exempel:

		PrintName("Fredrik", "Johansson");

		Skriver ut: Fredrik Johansson 
		*/

		string firstName, lastName;

		firstName = GetTextInput("förnamn");
		lastName = GetTextInput("efternamn");

		PrintName(firstName, lastName);

		
		static void PrintName(string firstName, string lastName)
		{
			Console.WriteLine($"{firstName} {lastName}");
		}
	}

	public static void Funktioner2()
	{
		/* Slå ihop för- och efternamn - returnera
		Skriv om funktionen ovan så att den istället för att skriva ut namnet returnerar en string med hela namnet. Anropa funktionen och skriv ut returvärdet.
		*/

		string firstName, lastName;

		firstName = GetTextInput("förnamn");
		lastName = GetTextInput("efternamn");

		Console.WriteLine(ReturnName(firstName, lastName));

		
		static string ReturnName(string firstName, string lastName)
		{
			return $"{firstName} {lastName}";
		}
	}

	public static void Funktioner3()
	{
		/* Kolla om stängen är längre än ett givet antal tecken.
		Skriv en funktion som tar in en sträng och ett heltal. Om längden på strängen är större än heltalet returnera true, annars false.
		*/

		string text = GetTextInput("text", false);

		Console.Write("Mata in ett heltal: ");
		GetInt(out int number, min: 0);

		Console.WriteLine(IsLonger(text, number) ? "Textens längd är högre än talet": "Talet är större än textens längd");

		static bool IsLonger(string text, int number)
		{
			return text.Length > number;
		}
	}

	public static void Funktioner4()
	{
		/* Omvandla Celsius till Fahrenheit
		Skriv en funktion som översätter ett värde från Celsius till Fahrenheit. Både in-parameter och returvärde ska vara av datatyp double.
		*/

		while(true)
		{
			string convertFrom = GetTemperatureScale(GetChar("vilken temperaturskala du vill konvertera FRÅN (C, F, K)", Char.IsLetter));
			Console.WriteLine();
			if(convertFrom == string.Empty)
			{
				Console.WriteLine("Inte en godkänd temperaturskala");
				continue;
			}

			string convertTo = GetTemperatureScale(GetChar("vilken temperaturskala du vill konvertera TILL (C, F, K)", Char.IsLetter));
			Console.WriteLine();
			if(convertTo == string.Empty)
			{
				Console.WriteLine("Inte en godkänd temperaturskala");
				continue;
			}

			if(convertFrom[0] == convertTo[0])
			{
				Console.WriteLine("Du kan inte konvertera till samma temperaturskala");
				continue;
			}

			Console.Write($"Mata in grader i {convertFrom}: ");
			if(!GetDouble(out double grader, min: GetMinValue(convertFrom[0])))
			{
				Console.WriteLine("Temperaturen är utanför temperaturskalan");
				continue;
			}

			Console.WriteLine($"{grader}°{convertFrom[0]} motsvarar {ConvertTempScale(convertFrom[0], convertTo[0], grader)}°{convertTo[0]}");

			PromptContinue();
		}

		static string GetTemperatureScale(char scale) =>		scale == 'C' ? "Celsius" : scale == 'F' ? "Fahrenheit" : scale == 'K' ? "Kelvin" : string.Empty;
		static double ConvertTempScale(char from, char to, double degrees)
		{
			switch(from)
			{
				case 'C':
					if(to == 'F')
					{
						return degrees * 9 / 5 + 32;
					}
					else
					{
						return degrees + 273.15;
					}

				case 'F':
					if(to == 'C')
					{
						return (degrees - 32) * 5 / 9;
					}
					else
					{
						return (degrees - 32) * 5 / 9 + 273.15;
					}

				default:
					if(to == 'C')
					{
						return degrees - 273.15;
					}
					else
					{
						return ( degrees - 273.15 ) * 9 / 5 + 32;
					}
			}
		}
		static double GetMinValue(char scale) => scale == 'C' ? -273.15 : scale == 'F' ? -459.67 : 0;
	}

}
