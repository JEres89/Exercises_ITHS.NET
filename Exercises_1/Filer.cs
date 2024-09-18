using static CommonFunctionality.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Exercises_1.FiletypeSpecifications;

namespace Exercises_1;

class Filer : Exercises
{
	private static Filer? filer = new();
	private Filer()
	{
		string name = "Filer";
		Dictionary<int, (Action, string)> exercises = new() {
			{ 1, (Filer1, "Läs in metadata från bildfiler") },

		};
		Init(name, exercises);
	}

	public static void UseFiler()
	{
		filer ??= new Filer();
	}

	// Add your exercise methods here

	public static void Filer1()
	{
		/*
		 * 
		 */

		string path;

		path = GetTextInput("en sökväg till en bild-fil", TextTypes.filePath);

		Console.WriteLine($"Sökvägen är: {path}");

		//var bytes = File.ReadAllBytes(path);
  //      Console.ReadLine();
		FileStream reader = File.OpenRead(path);


		ImageHeader? format = ImageHeader.DecodeFileSignature(reader);
		if(format == null)
		{
			Console.WriteLine("Okänt filformat");
		}
		else
		{
			if(format.DecodeHeader(reader))
			{
				Console.WriteLine("\n----");
                Console.WriteLine($"Bilden är {format.Height} pixlar hög och {format.Width} pixlar bred.");
				Console.WriteLine($"Bildformatet är: {format.GetImageFormat()}");
				Console.WriteLine("----\n");

			}
		}
		reader.Close();
	}
}
