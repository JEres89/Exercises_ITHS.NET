using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Exercises_1.FiletypeSpecifications;
internal abstract record ImageHeader
{
	public abstract bool BigEndian { get; }
	public abstract ReadOnlySpan<byte> Signature { get; }
	public uint Width { get; protected set; }
	public uint Height { get; protected set; }
	public abstract byte BitDepth { get; protected set; }


	internal uint ReadUint(Span<byte> bytes)
	{
		if(BigEndian)
		{
			bytes.Reverse();
		}
		switch (bytes.Length)
		{
			case 2:
				return BitConverter.ToUInt16(bytes);
			case 4:
				return BitConverter.ToUInt32(bytes);
			default:
				throw new Exception("Argument out of range of supported values");
		}
	}
	internal abstract bool DecodeHeader(FileStream stream);

	internal abstract string GetImageFormat();

	#region Static methods

	public static ReadOnlyDictionary<byte[], Func<ImageHeader>> SupportedImageFormats = new(new Dictionary<byte[], Func<ImageHeader>>
		{
			//{ new byte[] { 0xFF, 0xD8 }, "JPEG" },
			{ PngHeader.PngSignature, () => new PngHeader() },
			//{ new byte[] { 0x47, 0x49, 0x46, 0x38 }, "GIF" },
			{ BmpHeader.BmpSignature, () => new BmpHeader() },
			////{ new byte[] { 0x4D, 0x42 }, "BMP_revEndian" },
			//{ new byte[] { 0x00, 0x00, 0x01, 0x00 }, "ICO" }
		});

	internal static ImageHeader? DecodeFileSignature(FileStream reader)
	{
		var signature = new byte[4];
		reader.Read(signature, 0, 2);

		List<byte[]> supported = SupportedImageFormats.Keys.ToList();

		supported = FindMatches(signature, supported, 0, 2);

		if(supported.Count == 1)
		{
			var supportedSignature = supported[0];
			if(supportedSignature.Length == 2)
			{
				return SupportedImageFormats[supportedSignature]();
			}
		}

		reader.Read(signature, 2, 2);
		supported = FindMatches(signature, supported, 2, 2);

		if(supported.Count == 1)
		{
			if(signature.Length < supported[0].Length)
			{
				signature = new byte[supported[0].Length];
				reader.Position = 0;
				reader.Read(signature);

				if(!signature.SequenceEqual(supported[0]))
				{
					return null;
				}
			}
			return SupportedImageFormats[supported[0]]();
		}
		return null;

		static List<byte[]> FindMatches(byte[] signature, List<byte[]> supported, int start, int length)
		{
			int end = start + length;
			int index = start;

			for(; index < end ; index++)
			{
				byte signatureByte = signature[index];
				List<byte[]> newSupported = new List<byte[]>();
				foreach(var format in supported)
				{
					if(format[index] == signatureByte)
					{
						newSupported.Add(format);
					}
				}
				supported = newSupported;
				if(supported.Count == 0)
				{
					return supported;
				}
			}
			return supported;
		}
	}
	#endregion
}

internal record PngHeader : ImageHeader
{
	public override bool BigEndian { get; } = true;
	public override ReadOnlySpan<byte> Signature => PngSignature;
	//public override ReadOnlySpan<byte> Header => pngHeader;
	public static readonly byte[] PngSignature = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
	static readonly byte[] pngHeader = { 73, 72, 68, 82, }; // 0x49, 0x48, 0x44, 0x52,
	static readonly ReadOnlyCollection<byte> AllowedBitDepths = new([1, 2, 4, 8, 16]);
	static readonly ReadOnlyDictionary<byte, (string colourType, IList<byte> bitDepths)> ColourConfigurations = new ( new Dictionary<byte, (string, IList<byte>)>()
		{
			{ 0, ("Greyscale",  [1, 2, 4, 8, 16] ) },
			{ 2, ("Truecolor", [ 8, 16]) },
			{ 3, ("Indexed-color", [1, 2, 4, 8]) },
			{ 4, ("Greyscale with alpha", [ 8, 16]) },
			{ 6, ("Truecolor with alpha", [ 8, 16]) }
		});

	private byte bitDepth;
	private byte colorType;
	private byte compressionMethod;
	private byte filterMethod;
	private byte interlaceMethod;

	public override byte BitDepth {
		get => bitDepth;
		protected set {
			if(!AllowedBitDepths.Contains(value))
			{
				throw new ArgumentException("Invalid bit depth");
			}
			bitDepth = value;
		}
	}
	public byte ColorType { 
		get => colorType;
		private set {
			if(!ColourConfigurations.TryGetValue(value, out var colour))
			{
				throw new ArgumentException("Invalid color type");
			}
			if(!colour.bitDepths.Contains(bitDepth))
			{
				throw new ArgumentException("Invalid bit depth");
			}
			colorType = value;
		}
	}
	public byte CompressionMethod { 
		get => compressionMethod;
		private set {
			if(value != 0)
			{
				throw new ArgumentException("Invalid compression method");
			}
			compressionMethod = value;
		}
	}
	public byte FilterMethod { 
		get => filterMethod;
		private set {
			if(value != 0)
			{
				throw new ArgumentException("Invalid filter method");
			}
			compressionMethod = value;
		}
	}
	public byte InterlaceMethod { 
		get => interlaceMethod;
		private set {
			if(value > 1)
			{
				throw new ArgumentException("Invalid interlace method");
			}
			compressionMethod = value;
		}
	}

	//public PngHeader()
	//{
	//}
	private bool VerifyHeader(FileStream stream)
	{
		var expectedHeader = pngHeader;
		byte[] header = new byte[expectedHeader.Length];
		stream.Read(header, 0, expectedHeader.Length);

		return header.AsSpan().SequenceEqual(expectedHeader);
	}
	internal override bool DecodeHeader(FileStream stream)
	{
		Span<byte> bytes = stackalloc byte[4];
		stream.Read(bytes);
		uint headerSize = ReadUint(bytes);

		if(!VerifyHeader(stream))
		{
			Console.WriteLine("Ogiltig PNG-header");
			return false;
		}
		stream.Read(bytes);
		Width = ReadUint(bytes);
		stream.Read(bytes);
		Height = ReadUint(bytes);

		try
		{
			BitDepth = (byte)stream.ReadByte();
			ColorType = (byte)stream.ReadByte();
			CompressionMethod = (byte)stream.ReadByte();
			filterMethod = (byte)stream.ReadByte();
			interlaceMethod = (byte)stream.ReadByte();
		}
		catch(Exception e)
		{
			Console.WriteLine("Ogiltig PNG-header");
			return false;
		}
		return true;
	}

	internal override string GetImageFormat()
	{
		return $"PNG, {ColourConfigurations[colorType].colourType}, {BitDepth} bitar {(interlaceMethod == 1 ? "interlaced" : "icke interlaced" )}";
	}
}


internal record BmpHeader : ImageHeader
{
	public override bool BigEndian { get; } = false;
	public override ReadOnlySpan<byte> Signature => BmpSignature;
	public static readonly byte[] BmpSignature = { 0x42, 0x4D };

	static readonly ReadOnlyCollection<byte> AllowedBitDepths = new([1, 4, 8, 24]);

	private uint fileSize;
	private byte bitDepth;
	private uint numberOfColors;
	private uint numberOfImportantColors;
	private byte compressionMethod;

	public override byte BitDepth {
		get => bitDepth;
		protected set {
			if(!AllowedBitDepths.Contains(value))
			{
				throw new ArgumentException("Invalid bit depth");
			}
			bitDepth = value;
		}
	}
	public uint NumColors { 
		get => numberOfColors;
		private set => numberOfColors = value;
	}
	public uint NumImportantColors { 
		get => numberOfImportantColors;
		private set => numberOfImportantColors = value;
	}
	public byte CompressionMethod { 
		get => compressionMethod;
		private set {
			if(value > 2)
			{
				throw new ArgumentException("Invalid compression method");
			}
			compressionMethod = value;
		}
	}
	
	private bool VerifyHeader(FileStream stream)
	{
		Span<byte> bytes = stackalloc byte[4];
		stream.Read(bytes);
		if(ReadUint(bytes) != 0)
		{
			return false;
		}
		stream.Read(bytes);
		uint imageStartOffset = ReadUint(bytes);
		stream.Read(bytes);
		uint infoHeader = ReadUint(bytes);
		return infoHeader == 40;
	}

	internal override bool DecodeHeader(FileStream stream)
	{
		Span<byte> bytes = stackalloc byte[4];
		stream.Read(bytes);
		fileSize = ReadUint(bytes);

		if (!VerifyHeader(stream))
		{
			Console.WriteLine("Ogiltig BMP-header");
			return false;
		}
		stream.Read(bytes);
		Width = ReadUint(bytes);
		stream.Read(bytes);
		Height = ReadUint(bytes);

		try
		{
			bytes = stackalloc byte[2];
			stream.Read(bytes);
			ushort planes = (ushort)ReadUint(bytes);
			if (planes != 1)
			{
				throw new Exception($"invalid planes value: {planes}");
			}
			stream.Read(bytes);
			uint temp = ReadUint(bytes);
			BitDepth = (byte)(temp > 24 ? 255 : temp);

			bytes = stackalloc byte[4];
			stream.Read(bytes);
			temp = ReadUint(bytes);
			CompressionMethod = (byte)(temp > 255 ? 255 : temp);

			stream.Read(bytes); // discard size of image data
			stream.Read(bytes); // discard horizontal pixels / meter
			stream.Read(bytes); // discard vertical pixels / meter
			
			stream.Read(bytes);
			NumColors = ReadUint(bytes);

			stream.Read(bytes);
			NumColors = ReadUint(bytes);
		}
		catch(Exception e)
		{
			Console.WriteLine("Ogiltig PNG-header:");
			Console.WriteLine(e.Message);
			return false;
		}
		return true;
	}

	internal override string GetImageFormat()
	{
		return $"BMP, {NumColors} färger, {NumImportantColors} viktiga färger, {BitDepth} bitar. {fileSize} bytes stor";
	}
}

