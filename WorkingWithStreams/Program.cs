using Packt.Shared; // To use Viper
using System.Xml; // to use XmlWriter and the rest
#region Writing to text streams

SectionTitle("Writing to text streams");

// Define a file to write to
string textFile = Combine(Directory.GetCurrentDirectory(), "streams.txt");

// Create a text file and return a helper writer
StreamWriter text = File.CreateText(textFile);

// Enumerate the strings, writing each one to the stream on a separate line

foreach( string item in Viper.CallSigns)
{
    text.WriteLine(item);
}
text.Close(); // Release unmanaged file resources

OutputFileInfo(textFile);

/*
 * Remember that if you run the program from the command line with dotnet run, it will say current directory is the program directory
 * rather than the bin/debug
 */

#endregion
#region Writing to XML streams
string xmlFile = Combine(Directory.GetCurrentDirectory(), "streams.xml");

//  Declare variables to try and properly initialize later
FileStream? xmlFileStream = null;
XmlWriter? xmlWriter = null;

try
{
    xmlFileStream = File.Create(xmlFile);

    // Wrap the stream in an XML writer helper and tell it to automatically indent nested elements
    xmlWriter = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });

    // Write the XML declaration
    xmlWriter.WriteStartDocument();

    // Write a root element
    xmlWriter.WriteStartElement("callsigns");

    // Enumerate the strings, writing each to the stream
    foreach (string item in Viper.CallSigns)
    {
        xmlWriter.WriteElementString("callsign", item);
    }

    xmlWriter.WriteEndElement();
}
catch (Exception ex)
{
    // If the path doesn't exist the exception will be caught/
    WriteLine($"{ex.GetType} says {ex.Message}");
}
finally
{
    if (xmlWriter != null)
    {
        xmlWriter.Close();
        WriteLine("The XML writer's unmanaged resources have been disposed.");
    }
    if (xmlFileStream is not null)
    {
        xmlFileStream.Close();
        WriteLine("The XML file stream's unmanaged resources have been disposed.");
    }
}
OutputFileInfo(xmlFile);

// Good practice: Before calling the dispose method, make sure its not null
#endregion
#region Simplifying disposal by using the using statement
string path = Combine(Directory.GetCurrentDirectory(), "file2.txt");

using (FileStream file2 = File.OpenWrite(path))
{
    using (StreamWriter writer = new StreamWriter(file2))
    {
        try
        {
            writer.WriteLine("Welcome, .NET!");
        }
        catch (Exception ex)
        {
            WriteLine($"{ex.GetType()} says {ex.Message}");
        }
    } // Automatically calls Dispose if the object is not null. Handy and pretty <3
}// Automatically calls Dispose if the object is not null. Handy and pretty <3

// But guess what? If you wanna get freaky you can leave off the curly braces!

using FileStream file3 = File.OpenWrite(path);
using StreamWriter writer2 = new(file3);
try
{
    writer2.WriteLine("Look, more concise but Im not sure if I like it. OK I think I like it.");
}
catch (Exception ex)
{
    WriteLine($"{ex.GetType()} says {ex.Message}");
}


#endregion
#region Compressing streams

#endregion