using static CommonFunctionality.Common;
namespace Exercises_1;

internal class Program
{
	private static Dictionary<int, (string name, Exercises singleton)> _exerciseSingletons;
	/// <summary>
	/// 
	/// </summary>
	/// <param name="args"></param>
	static void Main(string[] args)
	{
		Console.WriteLine("Hello World!");
		Console.WriteLine("Let's look at some C# exercises.");

		ViewExercises();

	}

	private static void PopulateSingletons()
	{
		Variabler.UseVariabler();
		Loopar.UseLoopar();
		Indexering.UseIndexering();
		Funktioner.UseFunktioner();
		Euler.UseEuler();
		Filer.UseFiler();

		_exerciseSingletons = Exercises.GetSingletons();
	}

	public static void ViewExercises()
	{
		PopulateSingletons();

		while(true)
		{			
			Console.WriteLine("Vilka övningar vill du visa?");
			Console.WriteLine("0 Exit");
			foreach(var exercises in _exerciseSingletons)
			{
				Console.WriteLine($"{exercises.Key}. {exercises.Value.name}");
			}

			var input = Console.ReadLine();

			if(int.TryParse(input, out var exerciseNumber))
			{
				if(_exerciseSingletons.ContainsKey(exerciseNumber))
				{
					_exerciseSingletons[exerciseNumber].singleton.Invoke();
				}
				else if(exerciseNumber == 0)
				{
					Console.Clear();
					Console.WriteLine("Hej då!");
					break;
				}
				else
				{
					Console.WriteLine("Invalid number");
				}
			}
			else
			{
				try
				{
					_exerciseSingletons.First(x => x.Value.name == input).Value.singleton.Invoke();
				}
				catch(Exception)
				{
					Console.WriteLine("Invalid input");
					continue;
				}
			}
			PromptContinue();
		}
	}
}