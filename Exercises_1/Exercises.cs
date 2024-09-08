using static CommonFunctionality.Common;

namespace Exercises_1;

internal abstract class Exercises
{
	protected void Init(string name, Dictionary<int, (Action, string)> exercises)
	{
		_name = name;
		_exercises = exercises;
	}

	private string _name;
	private Dictionary<int, (Action,string)> _exercises;

	public static Dictionary<int, (string name, Exercises singleton)> GetSingletons()
	{
		return new()
		{
			{ 1, ("Variabler", Variabler.GetVariabler()) },
			{ 2, ("Loopar", Loopar.GetLoopar()) },
			{ 3, ("Indexering", Indexering.GetIndexering()) },
			{ 4, ("Funktioner", Funktioner.GetFunktioner()) },
			{ 5, ("Euler", Euler.GetEuler()) }
		};
	}

	public void Invoke()
	{
		while(true)
		{
			Console.WriteLine($"Vilken övning från {_name} vill du visa?");
			Console.WriteLine("0 Tillbaka till index");
			foreach(var exercise in _exercises)
			{
				Console.WriteLine($"{exercise.Key}. {exercise.Value.Item2}");
			}

			int exerciseNumber;

			if(GetInt(out exerciseNumber, 0, _exercises.Count))
			{
				if(_exercises.ContainsKey(exerciseNumber))
				{
					_exercises[exerciseNumber].Item1();
				}
				else
				{
					Console.WriteLine("Tillbaka till index");
					break;
				}
			}
			else
			{
				Console.WriteLine("Invalid number");
				PromptContinue();
			}
		}
	}
}
