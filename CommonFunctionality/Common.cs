namespace CommonFunctionality;

public static class Common
{
	public static void PromptContinue()
	{
		Console.WriteLine("Press enter to continue...");
		_ = Console.ReadLine();
		Console.Clear();
	}

	public static bool GetInt(out int output, int min = -1, int max = int.MinValue)
	{
		max = min > max ? min : max;
		while(!int.TryParse(Console.ReadLine(), out output))
		{
			Console.WriteLine("Invalid input");
		}
		if(min != max)
		{
			return output >= min && output <= max;
		}
		else if(min > -1)
		{
			return output > min;
		}
		return true;
	}
	public static bool GetDouble(out double output, double min = -1, double max = double.MinValue)
	{
		max = min > max ? min : max;
		while(!double.TryParse(Console.ReadLine(), out output))
		{
			Console.WriteLine("Invalid input");
		}
		if(min != max)
		{
			return output >= min && output <= max;
		}
		else if(min != -1)
		{
			return output > min;
		}
		return true;
	}

	public static char GetChar(string description, Predicate<char> match)
	{
		Console.Write($"Mata in {description}: ");

		char c;
		while(true)
		{
			c = Console.ReadKey().KeyChar;
			Console.WriteLine();
			if(match(c))
			{
				return c;
			}
			else
			{
				Console.WriteLine($"Ogiltig input!");
			}
		} 
	}
	public static string GetTextInput(string denomination, bool isName = true)
	{
		Console.Write($"Mata in {denomination}: ");
		while(true)
		{
			var text = Console.ReadLine()?.Trim();
			if(isName && !IsValidName(text))
			{
				Console.WriteLine($"Ogiltigt {denomination}!");
				Console.Write($"Mata in {denomination}: ");
			}
			else
			{
				return text!;
			}
		}
	}
	private static bool IsValidName(string? name)
	{
		return !string.IsNullOrWhiteSpace(name) && Char.IsUpper(name[0]) && !name.Any(Char.IsNumber);
	}
}
