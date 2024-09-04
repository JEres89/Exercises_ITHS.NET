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
			{ 4, Funktioner4 },
			{ 5, Funktioner5 },

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
		PromptContinue();


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
		PromptContinue();


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
		PromptContinue();

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

		string convertFrom,convertTo;
		double grader;

		while(true)
		{
			convertFrom = GetTemperatureScale(GetChar("vilken temperaturskala du vill konvertera FRÅN (C, F, K)", Char.IsLetter));
			Console.WriteLine();
			if(convertFrom == string.Empty)
			{
				Console.WriteLine("Inte en godkänd temperaturskala");
				continue;
			}
			break;
		}

		while(true)
		{
			convertTo = GetTemperatureScale(GetChar("vilken temperaturskala du vill konvertera TILL (C, F, K)", Char.IsLetter));
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
			break;
		}

		while(true)
		{
			Console.Write($"Mata in grader i {convertFrom}: ");
			if(!GetDouble(out grader, min: GetMinValue(convertFrom[0])))
			{
				Console.WriteLine("Temperaturen är utanför temperaturskalan");
				continue;
			}
			break;
		}

		Console.WriteLine($"{grader}°{convertFrom[0]} motsvarar {ConvertTempScale(convertFrom[0], convertTo[0], grader)}°{convertTo[0]}");
		PromptContinue();

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

	public static void Funktioner5()
	{
        /* Lägg in bindesträck mellan tecken i en sträng
		Skriv en funktion som tar in en sträng och returnerar en sträng med ett bindestreck mellan varje tecken. T.ex om man skickar in “Fredrik” , så returnerar den “F-r-e-d-r-i-k”
		*/

        Console.WriteLine("Skriv en text som ska perforeras med ett annat tecken:");
		string text = GetTextInput("text", false);

		char c = GetChar("vilket tecken du vill ha som bindestecken", c => !Char.IsWhiteSpace(c));
		char[] perforatedText = new char[text.Length*2-1];
		int perforatedIndex = 0;

		for(int i = 0 ; i < text.Length ; i++)
		{
			if(char.IsWhiteSpace(text[i]))
			{
				perforatedText[perforatedIndex] = text[i];
				perforatedIndex++;
				continue;
			}
			perforatedText[perforatedIndex] = text[i];
			perforatedIndex++;
			if(i+1 < text.Length)
			{
				if(char.IsWhiteSpace(text[i + 1]))
				{
					continue;
				}
				perforatedText[perforatedIndex] = c;
				perforatedIndex++;
			}
		}

		string output = new string(perforatedText, 0, perforatedIndex);

		Console.WriteLine(output);
		PromptContinue();
	}

	public static void Funktioner6()
	{
		/* Egen version av String.Join();
		Skriv en egen funktion som fungerar på samma sätt som String.Join();
		*/

		Console.WriteLine("Skriv in ett antal ord som ska bindas ihop av tecken");

		foreach (var item in GetTextInput("text", false))
		{

		}
		//string[] text = 



		PromptContinue();
	}
}
