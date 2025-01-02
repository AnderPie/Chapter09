using System.Text; // To use Encoding

#region Encoding and Decoding text
/*
 * Encoding          | Description
 * ASCII             | This encodes a limited range of characters using the lower seven bits of a byte
 * UTF-8             | This represents each Unicode code point as a sequence of one to four bytes
 * UTF-7             | This is designed to be more efficient over 7-bit channels than UTF-8, but it has security and robustness issues. Don't use it.
 * UTF-16            | This represents each Unicode code point as a sequence of one or two 16-bit integers
 * UTF-32            | Each code point represented as a 32-bit integer, only fixed-length Unicode encoding, all others are variable length
 * ANSI/ISO          | This provides support for a variety of code pages that are used to support a specific language or group of languages
 */

WriteLine("Encodings");
WriteLine("[1] ASCII");
WriteLine("[2] UTF-7");
WriteLine("[3] UTF-8");
WriteLine("[4] UTF-16 (Unicode)");
WriteLine("[5] UTF-32");
WriteLine("[6] Latin1");
WriteLine("[any other key] Default Encoding");
WriteLine();

Write("Press a number to choose an encoding.");
ConsoleKey number = ReadKey(intercept: true).Key;
WriteLine(); WriteLine();

Encoding encoder = number switch
{
    ConsoleKey.D1 or ConsoleKey.NumPad1 => Encoding.ASCII,
    ConsoleKey.D2 or ConsoleKey.NumPad2 => Encoding.UTF7, // .NET does not like this, because it is not secure
    ConsoleKey.D3 or ConsoleKey.NumPad3 => Encoding.UTF8,
    ConsoleKey.D4 or ConsoleKey.NumPad4 => Encoding.Unicode,
    ConsoleKey.D5 or ConsoleKey.NumPad5 => Encoding.UTF32,
    ConsoleKey.D6 or ConsoleKey.NumPad6 => Encoding.Latin1,
    _ => Encoding.Default
};

// Define a string to encode
string message = "Coffee $9.99";
WriteLine($"Text to encode: {message} Characters: {message.Length}");

// Encode the string to a byte array
byte[] encoded = encoder.GetBytes(message);

// Check how many bytes the encoding neeed
WriteLine($"{encoder.GetType().Name} used {encoded.Length} bytes");
WriteLine();

//Enumerate each byte
WriteLine("BYTE | HEX | CHAR");
foreach (byte b in encoded)
{
    WriteLine($"{b,4}  | {b,3:X} | {(char)b,4}");
}

// Decode the byte array back into a  string and display it

string decoded = encoder.GetString(encoded);
WriteLine($"Decoded: {decoded}");

// Note that you can specify an encoding for a StreamReader or StreamWriter object, like so:
//StreamReader reader = new(someStream, Encoding.UTF8);
//StreamWriterwriter = new(someStream, Encoding.UTF8);
#endregion

