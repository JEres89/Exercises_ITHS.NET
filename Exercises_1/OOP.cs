using static CommonFunctionality.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises_1;

/// <summary>
/// Encapsulation, gruppera information och funktionalitet som hör ihop
/// Abstraction, gömma/sammanfatta detaljer till en enkel representation
/// Inheritance, dela gemensam abstraherad form för olika detaljerade implementationer
/// Polymorphism, omdefiniera information och funktionalitet
/// 
/// Egenskaper, information som går att ses utifrån, t.ex. färg, storlek, etc.
/// Interna tillstånd, information som är dold men kan påverka hur objekter beter sig
/// Metoder, funktionalitet som objekt kan utföra
/// 
/// 
/// </summary>
class OOP : Exercises
{
	private static OOP? oop = new();
	private OOP()
	{
		string name = "OOP";
		Dictionary<int, (Action, string)> exercises = new()
		{
			{1, (OOP1, "OOP1")},
			{2, (KatterMini, "KatterMini")},
			// Add your exercises here
		};
		Init(name, exercises);
	}

	public static void UseOOP()
	{
		oop ??= new OOP();
	}

	// Add your exercise methods here
	public static void KatterMini()
	{
		Cat[] cats = new Cat[100];

		for (int i = 0; i < cats.Length; i++)
		{
			cats[i] = new Cat() { Name = $"Katt{i+1}", Age = i+1};
			Console.WriteLine($"{cats[i].Name} är {cats[i].Age} år gammal");
		}

	}
	private class Cat
	{
        public string Name { get; set; }
		public int Age { get; set; }

	}

	public static void OOP1()
	{

	}
}
