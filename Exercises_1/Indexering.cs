using static CommonFunctionality.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises_1;

class Indexering : Exercises
{
	private static Indexering? indexering;
	private Indexering()
	{
		string name = "Indexering";
		Dictionary<int, (Action, string)> exercises = new()
		{
			{ 1, (Indexering1, "En bokstav på varje rad") }
		};
		Init(name, exercises);
	}


	public static Indexering GetIndexering()
	{
		return ( indexering ??= new Indexering() );
	}

	public static void Indexering1()
	{
		/* En bokstav på varje rad
		Be användaren mata in en sträng. Skriv ut varje tecken i strängen på en egen rad.
		*/


	}
}