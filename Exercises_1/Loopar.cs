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
		int x, y, d;
		string? input;

		Console.Write("Mata in höjd: ");
		do
		{
			input = Console.ReadLine();
		} while(!ParseInput(input, out y));

		Console.Write("Mata in bredd: ");
		do
		{
			input = Console.ReadLine();
		} while(!ParseInput(input, out x));

		Console.Write("Mata in kanttjocklek: ");
		do
		{
			input = Console.ReadLine();
		} while(!ParseInput(input, out d));

		d = d < 2 ? 
			1 : 
			( d > x / 2 ) || ( d > y / 2 ) ? 
				x < y ? 
					x / 2 : 
					y / 2 : 
				d ;

		PrintBox(x, y, d);
	}

	private static void PrintBox(int x, int y, int d)
	{

		string edgeRow = new string('X', x);
		string middleRow = new StringBuilder(x).Append('X', d).Append(' ', x - 2 * d).Append('X', d).ToString();

		for(int i = 0 ; i < y ; i++)
		{
			if(i < d || i >= y - d)
			{
                Console.WriteLine(edgeRow);
			}
			else
			{
				Console.WriteLine(middleRow);
			}
		}
        Console.ReadLine();
	}

	private static bool ParseInput(string? input, out int result)
	{
		if(int.TryParse(input, out result))
		{
			return true;
		}
		Console.WriteLine("Invalid number");

		return false;
	}
}
