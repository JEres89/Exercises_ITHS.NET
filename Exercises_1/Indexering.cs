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
		Dictionary<int, Action> exercises = new()
		{
			{ 1, Indexering1 }
		};
		Init(name, exercises);
	}


	public static Indexering GetIndexering()
	{
		return ( indexering ??= new Indexering() );
	}

	public static void Indexering1()
	{
		/* 
		*/


	}
}