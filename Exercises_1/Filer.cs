using static CommonFunctionality.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises_1;

class Filer : Exercises
{
	private static Filer? filer = new();
	private Filer()
	{
		string name = "Filer";
		Dictionary<int, (Action, string)> exercises = new()
			{
			// Add your exercises here
		};
		Init(name, exercises);
	}

	public static void UseFiler()
	{
		filer ??= new Filer();
	}

	// Add your exercise methods here
}
