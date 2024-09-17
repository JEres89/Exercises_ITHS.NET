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
	
	//static ReadOnlyDictionary<string, Action<FileStream>> DecodeMethods = new( new Dictionary<string, Action<FileStream>>
	//	{
	//		{ "JPEG", DecodeJPEG },
	//		{ "PNG", DecodePNG },
	//		{ "GIF", DecodeGIF },
	//		{ "BMP", DecodeBMP },
	//		//{ "BMP_revEndian", DecodeBMP_revEndian },
	//		{ "ICO", DecodeICO }
	//	});

	private static void DecodeICO(FileStream stream)
	{
		throw new NotImplementedException();
	}

	private static void DecodeBMP(FileStream stream)
	{
		throw new NotImplementedException();
	}

	private static void DecodeGIF(FileStream stream)
	{
		throw new NotImplementedException();
	}

	//private static void DecodePNG(FileStream stream)
	//{
	//	byte[] pngHeader = { 0x49, 0x48, 0x44, 0x52, };

	//	if(!VerifyHeader(stream, pngHeader))
	//	{
	//		Console.WriteLine("Ogiltig PNG-header");
	//		return;
	//	}
	//	Span<byte> bytes = stackalloc byte[4];
	//	stream.Read(bytes);
	//	uint width = BitConverter.ToUInt32(bytes);
	//	stream.Read(bytes);
	//	uint height = BitConverter.ToUInt32(bytes);
	//	byte bitDepth = (byte)stream.ReadByte();
	//	byte colorType = (byte)stream.ReadByte();
	//	byte compressionMethod = (byte)stream.ReadByte();
	//	byte filterMethod = (byte)stream.ReadByte();
	//	byte interlaceMethod = (byte)stream.ReadByte();

	//	Console.WriteLine($"Upplösning BxH: {width}x{height}");
	//	Console.WriteLine($"Bitdjup: {bitDepth}");
	//	Console.WriteLine($"Färgtyp: {colorType}");
	//	Console.WriteLine($"Komprimeringsmetod: {compressionMethod}");
	//	Console.WriteLine($"Filtermetod: {filterMethod}");
	//	Console.WriteLine($"Interlacing: {interlaceMethod}");
	//}

	//private static bool VerifyHeader(FileStream stream, ImageHeader expectedHeader)
	//{
	//	byte[] header = new byte[expectedHeader.Header.Length];
	//	stream.Read(header, 0, expectedHeader.Header.Length);

	//	return header.SequenceEqual(expectedHeader.Header);
	//}

	/// <summary>
	/// JPEG decoding is a complete mess, not going to bother with it
	/// </summary>
	/// <param name="stream"></param>
	/// <exception cref="NotImplementedException"></exception>
	private static void DecodeJPEG(FileStream stream)
	{
        Console.WriteLine("Use a better image format!");
	}

	
}
