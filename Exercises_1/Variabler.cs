using static CommonFunctionality.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises_1;

class Variabler : Exercises
{
	private static Variabler? variabler;
	private Variabler()
	{
		string name = "Variabler";
		Dictionary<int, Action> exercises = new()
		{
			{ 1, Variabler1 }
		};
		Init(name, exercises);
	}


	public static Variabler GetVariabler()
	{
		return ( variabler ??= new Variabler() );
	}

	public static void Variabler1()
	{
		/* Hälsa på användaren
		Skriv ett program som frågar efter användarens namn och skriver ut en hälsning på konsolen.

		Exempel:

		Skriv ditt namn:
		David
		Hej David!
		*/

		Console.Write("Skriv ditt namn:");
		var name = Console.ReadLine();
		Console.WriteLine($"Hej {name}!");

	}
}