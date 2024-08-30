namespace Exercises_1;

internal class Program
{
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


	}
}
