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

		// Statements (satser):
		// 1. Declaration, Assignment
		int a = 5;
		int b;

		// 2. Expression statement
		b = a * a;

		// 3. Method call
		Console.WriteLine(b);

		// 4. Iteration
		for(int i = 0 ; i < 10 ; i++)
		{
			Console.WriteLine(i);
		}

		// 5. Selection
		if(a > 5)
		{
			Console.WriteLine("a is greater than 5");
		}
		else
		{
			Console.WriteLine("a is less than or equal to 5");
		}

		Exercises();

	}

	private static void PopulateSingletons()
	{
		_exerciseSingletons = new();
		_exerciseSingletons.Add(1, ("Loopar", Loopar.GetLoopar()));
		//_exerciseSingletons.Add(2, ("Metoder", typeof(Metoder)));
		//_exerciseSingletons.Add("Strängar", typeof(Strängar));
		//_exerciseSingletons.Add("Villkor", typeof(Villkor));
		//_exerciseSingletons.Add("Övrigt", typeof(Övrigt));
	}

	public static void Exercises()
	{
		PopulateSingletons();

		while(true)
		{
			Console.ReadKey();
			Console.Clear();
			Console.WriteLine("Vilka övningar vill du visa?");
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
		}
	}
}