using System.IO;

namespace CommonFunctionality;

public static class Common
{
	public static void PromptContinue()
	{
		Console.WriteLine("Press enter to continue...");
		_ = Console.ReadLine();
		Console.Clear();
	}

	public enum TextTypes {
		none = 0,
		name = 1,
		filePath = 2,
		directoryPath = 3,

	}
	public static Dictionary<int, string> NumberNames = new()
		{
			{ 0, "noll" },
			{ 1, "ett" },
			{ 2, "två" },
			{ 3, "tre" },
			{ 4, "fyra" },
			{ 5, "fem" },
			{ 6, "sex" },
			{ 7, "sju" },
			{ 8, "åtta" },
			{ 9, "nio" },
			{ 10, "tio" },
			{ 11, "elva" },
			{ 12, "tolv" },
			{ 13, "tretton" },
			{ 14, "fjorton" },
			{ 15, "femton" },
			{ 16, "sexton" },
			{ 17, "sjutton" },
			{ 18, "arton" },
			{ 19, "nitton" },
			{ 20, "tjugo" },
			{ 30, "trettio" },
			{ 40, "fyrtio" },
			{ 50, "femtio" },
			{ 60, "sextio" },
			{ 70, "sjuttio" },
			{ 80, "åttio" },
			{ 90, "nittio" },
			{ 100, "hundra" },
			{ 1000, "tusen" },
			{ 1000000, "miljon" },
			{ 1000000000, "miljard" }
		};

	public static bool GetInt(out int output, int min = int.MinValue, int max = int.MinValue, bool required = true)
	{
		max = min >= max ? int.MaxValue : max;
		while(!int.TryParse(Console.ReadLine(), out output))
		{
			if(!required)
			{
				return false;
			}
			Console.WriteLine("Invalid input");
		}
		if(min != max)
		{
			return output >= min && output <= max;
		}
		else if(min != int.MinValue)
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

	public static char GetCharInput(string description, Predicate<char> match)
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

	public static char[] GetCharsInput(string description, Predicate<char> match)
	{
		Console.Write($"Mata in {description}: ");

		Span<char> chars = stackalloc char[256];
		int index = 0;
		while(true)
		{
			char c = Console.ReadKey(true).KeyChar;
			if(match(c))
			{
				Console.Write(c);
				chars[index] = c;
			}
			else if(c == '\r')
			{
				Console.WriteLine();
				return chars.Slice(0, index).ToArray();
			}
			else
			{
				int cursorLeft = Console.CursorLeft;
				Console.SetCursorPosition(0, Console.CursorTop+1);
				Console.WriteLine($"Ogiltig input!");
				Console.SetCursorPosition(cursorLeft, Console.CursorTop-1);
			}
		}

	}

	public static string GetTextInput(string denomination, TextTypes inputType = TextTypes.none)
	{
		Console.Write($"Mata in {denomination}: ");
		while(true)
		{
			var text = Console.ReadLine()?.Trim();
			switch(inputType)
			{
				case TextTypes.none:
					return text!;
				case TextTypes.name:
					if(!IsValidName(text))
					{
						Console.WriteLine($"Ogiltigt {denomination}!");
						Console.Write($"Mata in {denomination}: ");
						continue;
					}
					return text!;
				case TextTypes.filePath:
					if(!File.Exists(text))
					{
						Console.WriteLine($"Ogiltigt format eller filen finns inte!");
						Console.Write($"Mata in {denomination}: ");
						continue;
					}
					return text!;
				case TextTypes.directoryPath:
					if(!Directory.Exists(text))
					{
						Console.WriteLine($"Ogiltigt format eller mappen finns inte!");
						Console.Write($"Mata in {denomination}: ");
						continue;
					}
					return text!;
				default:
					return text!;
			}
		}
	}

	private static bool IsValidName(string? name)
	{
		return !string.IsNullOrWhiteSpace(name) && Char.IsUpper(name[0]) && !name.Any(Char.IsDigit);
	}
	//private static bool IsValidPath(string? path)
	//{
	//	var invalidChars = Path.GetInvalidPathChars();
	//	if(!string.IsNullOrWhiteSpace(path) && !path.Any(c => invalidChars.Contains(c)))
	//	{
	//		return Path.Exists(path);
	//	}
	//	return false;
	//}
}
