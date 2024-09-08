using static CommonFunctionality.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises_1;

class Funktioner : Exercises
{
	private static Funktioner? funktioner;
	private Funktioner()
	{
		string name = "Funktioner";
		Dictionary<int, (Action,string)> exercises = new() {
			{ 1, (Funktioner1, "Slå ihop för- och efternamn - skriv ut") },
			{ 2, (Funktioner2, "Slå ihop för- och efternamn - returnera") },
			{ 3, (Funktioner3, "Kolla om stängen är längre än ett givet antal tecken") },
			{ 4, (Funktioner4, "Omvandla Celsius till Fahrenheit") },
			{ 5, (Funktioner5, "Lägg in bindesträck mellan tecken i en sträng") },
			{ 6, (Funktioner6, "Egen version av String.Join()") },
			{ 7, (Funktioner7, "Beräkna medelvärde av int-array") },
			{ 8, (Funktioner8, "Siffror till text") },
			{ 9, (Funktioner9, "Heltal till text") }
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

		Console.WriteLine(IsLonger(text, number) ? "Textens längd är högre än talet" : "Talet är större än textens längd");
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
			convertFrom = GetTemperatureScale(GetCharInput("vilken temperaturskala du vill konvertera FRÅN (C, F, K)", Char.IsLetter));
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
			convertTo = GetTemperatureScale(GetCharInput("vilken temperaturskala du vill konvertera TILL (C, F, K)", Char.IsLetter));
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

		static string GetTemperatureScale(char scale) => scale == 'C' ? "Celsius" : scale == 'F' ? "Fahrenheit" : scale == 'K' ? "Kelvin" : string.Empty;
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
						return ( degrees - 32 ) * 5 / 9;
					}
					else
					{
						return ( degrees - 32 ) * 5 / 9 + 273.15;
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

		char c = GetCharInput("vilket tecken du vill ha som bindestecken", c => !Char.IsWhiteSpace(c));

		Console.WriteLine(PerforateString(text, c));
		PromptContinue();

		static string PerforateString(string text, char c)
		{
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
				if(i + 1 < text.Length)
				{
					if(char.IsWhiteSpace(text[i + 1]))
					{
						continue;
					}
					perforatedText[perforatedIndex] = c;
					perforatedIndex++;
				}
			}

			return new string(perforatedText, 0, perforatedIndex);
		}
	}

	public static void Funktioner6()
	{
		/* Egen version av String.Join();
		Skriv en egen funktion som fungerar på samma sätt som String.Join();
		*/

		Console.WriteLine("Skriv in ett antal ord som ska bindas ihop av tecken");

		var text = GetTextInput("text", false);
		List<string> words = new List<string>();

		int wordStart = 0;
		for(int i = 0 ; i < text.Length ; i++)
		{
			if(char.IsWhiteSpace(text[i]))
			{
				words.Add(text.Substring(wordStart, i - wordStart));
				wordStart = i + 1;
			}
		}

		words.Add(text.Substring(wordStart));

		Console.WriteLine("Skriv in tecknen som ska binda ihop orden:");
		string joinString = GetTextInput("tecken", false);

		Console.WriteLine(JoinWords(words, joinString));

		PromptContinue();

		static string JoinWords(List<string> words, string s)
		{
			char[] result = new char[words.Sum(w => w.Length + s.Length) - s.Length];
			int wordIndex = 0;
			for(int i = 0 ; i < result.Length ; i++)
			{
				var word = words[wordIndex].AsSpan();
				for(int j = 0 ; j < word.Length; j++)
				{
					result[i] = word[j];
					i++;
				}
				if(wordIndex == words.Count - 1)
				{
					break;
				}
				for(int j = 0 ; j < s.Length ; j++)
				{
					result[i] = s[j];
					i++;
				}
				i--;
				wordIndex++;
			}
			return new string(result);
		}
	}

	public static void Funktioner7()
	{
        /* Beräkna medelvärde av int-array
		Skriv en funktion som tar en int[] in, och returnerar medelvärdet som en double.
		*/
        Console.WriteLine("Mata in ett antal ints som du vill ha medelvärdet av:");
		List<int> numbers = new List<int>();

		while(GetInt(out var number, required: false))
		{
			numbers.Add(number);
		}

		Console.WriteLine($"Medelvärdet är: {GetAverage(numbers.ToArray())}");

		PromptContinue();

		static double GetAverage(int[] numbers)
		{
			double average = 0;
			foreach(var number in numbers)
			{
				average += number;
			}
			return average / numbers.Length;
		}
	}

	public static void Funktioner8()
	{
		/* Siffror till text
		Skriv en funktion som tar ett heltal in, och returnerar en string[] där varje element innehåller ordet för varje siffra i talet.
		*/
		Console.WriteLine("Mata in ett heltal som ska skrivas ut med text:");

		GetInt(out var number);

		foreach(var numberName in GetDigitNames(number))
		{
			Console.Write(numberName);
			Console.Write(' ');
		}

		PromptContinue();

		static string[] GetDigitNames(int number)
		{
			char[] digits = number.ToString().ToCharArray();
			string[] digitNames = new string[digits.Length];
			for(int i = 0 ; i < digits.Length ; i++)
			{
				digitNames[i] = NumberNames[digits[i] - '0'];
			}
			return digitNames;
		}
	}

	public static void Funktioner9()
	{
		/* Heltal till text
		Skriv en funktion som tar en ushort som parameter, och returnerar en sträng med numret utskrivet i ord.
		*/
		Console.WriteLine("Mata in ett heltal mellan -32,767 och 32,767:");
		int number;
		while(!GetInt(out number, -32767, 32767))
		{
			Console.WriteLine($"Ogiltig tal!");
		}
		short shortNumber = (short)number ;

		Console.WriteLine(GetNumberName(shortNumber));

		PromptContinue();

		static string GetNumberName(short number)
		{
			char[] digits = number.ToString().ToCharArray();
			StringBuilder numberName = new();

			int i = 0;
			if(digits[0] == '-')
			{
				numberName.Append("Minus ");
				i++;
			}

			int num = 0;
			switch(digits.Length-i)
			{
				case 5:
					num = ( digits[i] - '0' ) * 10;
					i++;
					if(num == 10)
					{
						goto case 4;
					}

					numberName.Append(NumberNames[num]);
					num = 0;
					goto case 4;
				case 4:
					num += digits[i] - '0';
					i++;
					if(num == 0)
					{
						numberName.Append(' ');
						numberName.Append(NumberNames[1000]);
						numberName.Append(' ');
						goto case 3;
					}
					numberName.Append(NumberNames[num]);
					numberName.Append(' ');
					numberName.Append(NumberNames[1000]);
					numberName.Append(' ');
					num = 0;
					goto case 3;
				case 3:
					num = digits[i] - '0';
					i++;
					if(num == 0)
					{
						goto case 2;
					}
					numberName.Append(NumberNames[num]);
					numberName.Append(' ');
					numberName.Append(NumberNames[100]);
					numberName.Append(' ');
					num = 0;
					goto case 2;
				case 2:
					num = ( digits[i] - '0' ) * 10;
					i++;
					if(num == 10)
					{
						goto case 1;
					}

					numberName.Append(NumberNames[num]);
					numberName.Append(' ');
					num = 0;
					goto case 1;
				case 1:
					num += digits[i] - '0';
					i++;
					if(num == 0)
					{
						break;
					}
					numberName.Append(NumberNames[num]);
					break;
				default:
					break;
			}


			return numberName.ToString();
		}
	}

	public static void Funktioner10()
	{
		/* Hitta index för alla förekomster av ett givet tecken.
		Skapa funktionen int[] IndexesOf(string text, char c) som söker igenom strängen text och returnerar en int[] med index till alla förekomster av c i text.
		*/

		Console.WriteLine("Mata in :");


		//Console.WriteLine($": { }");

		PromptContinue();

		//static string ´Func( )
		//{

		//}
	}

	//public static void Funktioner()
	//{
	//	/* 

	//	*/
	//	Console.WriteLine("Mata in :");


	//	//Console.WriteLine($": { }");

	//	PromptContinue();

	//	//static string ´Func( )
	//	//{

	//	//}
	//}
}
