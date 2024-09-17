using static CommonFunctionality.Common;

namespace Exercises_1;

internal abstract class Exercises
{
	protected void Init(string name, Dictionary<int, (Action, string)> exercises)
	{
		_name = name;
		_exercises = exercises;
		_singletons.Add(_singletons.Count + 1, (name, this));
	}

	private string _name;
	private Dictionary<int, (Action,string)> _exercises;
	private static Dictionary<int, (string name, Exercises singleton)> _singletons = new();

	public static Dictionary<int, (string name, Exercises singleton)> GetSingletons()
	{
		return _singletons;
	}

	public void Invoke()
	{
		while(true)
		{
			Console.WriteLine($"Vilken övning från {_name} vill du visa?");
			Console.WriteLine("0 Tillbaka till index");
			int largestKey = 0;
			foreach(var exercise in _exercises)
			{
				Console.WriteLine($"{exercise.Key}. {exercise.Value.Item2}");
				largestKey = exercise.Key > largestKey ? exercise.Key : largestKey;
			}

			int exerciseNumber;

			if(GetInt(out exerciseNumber, 0, largestKey))
			{
				if(_exercises.TryGetValue(exerciseNumber, out (Action, string) exercise))
				{
					exercise.Item1();
				}
				else if(exerciseNumber == 0)
				{
					Console.WriteLine("Tillbaka till index");
					break;
				}
				else
				{
					Console.WriteLine("Övningen finns inte");
					PromptContinue();
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
