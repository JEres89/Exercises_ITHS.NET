using static CommonFunctionality.Common;

namespace Exercises_1;

internal abstract class Exercises
{
	protected void Init(string name, Dictionary<int, Action> exercises)
	{
		_name = name;
		_exercises = exercises;
	}

	private string _name;
	private Dictionary<int, Action> _exercises;

	public static Dictionary<int, (string name, Exercises singleton)> GetSingletons()
	{
		return new()
		{
			{ 1, ("Variabler", Variabler.GetVariabler()) },
			{ 2, ("Loopar", Loopar.GetLoopar()) },
			{ 3, ("Indexering", Indexering.GetIndexering()) },
			{ 4, ("Funktioner", Funktioner.GetFunktioner()) }
		};
	}

	//public static void PromptContinue()
	//{
	//	Console.WriteLine("Press enter to continue...");
	//	_ = Console.ReadLine();
	//}
	//public static bool GetInt(out int output, int min=-1, int max=-1)
	//{
	//	max = min > max ? min : max;
	//	while(!int.TryParse(Console.ReadLine(), out output))
	//	{
	//		Console.WriteLine("Invalid input");
	//	}
	//	if(min != max)
	//	{
	//		return output >= min && output <= max;
	//	}
	//	else if(min > -1)
	//	{
	//		return output > min;
	//	}
	//	return true;
	//}

	public void Invoke()
	{
		while(true)
		{
			Console.Clear();
			Console.WriteLine($"Vilken övning från {_name} vill du visa?");
			Console.WriteLine("0 Tillbaka till index");
			foreach(var exercise in _exercises)
			{
				Console.WriteLine(exercise.Key);
			}

			int exerciseNumber;

			if(GetInt(out exerciseNumber, 0, _exercises.Count))
			{
				if(_exercises.ContainsKey(exerciseNumber))
				{
					_exercises[exerciseNumber]();
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
			}

			PromptContinue();
		}
	}
}
