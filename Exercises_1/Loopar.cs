using static CommonFunctionality.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Exercises_1;

internal class Loopar : Exercises
{
	private static Loopar? loopar;
	private Loopar()
	{
		string name = "Loopar";
		Dictionary<int, (Action,string)> exercises = new()
		{
			{ 10, (Loopar10, "Ihålig box") }
		};
		Init(name, exercises);
	}

	public static void UseLoopar()
	{
		loopar ??= new Loopar();
	}

	public static void Loopar10()
	{
		/* Ihålig box
		Fråga efter bredd och höjd och skriv ut en “box” som inte är fylld.

		Exempel:
		Mata in höjd: 4
		Mata in bredd: 5
		XXXXX
		X   X
		X   X
		XXXXX
		*/
		int width, height, thickness;

		Console.Write("Mata in höjd: ");
		while(!GetInt(out height, 1, Console.WindowHeight));

		Console.Write("Mata in bredd: ");
		while(!GetInt(out width, 1, Console.WindowWidth));

		Console.Write("Mata in kanttjocklek: ");
		while (!GetInt(out thickness, 1, int.Min(width/2, height/2)));

		thickness = thickness < 2 ? 
			1 : 
			( thickness > width / 2 ) || ( thickness > height / 2 ) ? 
				width < height ? 
					width / 2 : 
					height / 2 : 
				thickness ;
		PrintBox(width, height, thickness);
	}
}
