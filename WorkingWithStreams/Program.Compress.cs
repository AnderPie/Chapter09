using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packt.Shared;
using System.IO.Compression; // To use BrotliStream, and GZipStream
using System.Xml; // Guess what we're using this for ;) 

partial class Program
{
    private static void Compress(string algorithm = "gzip")
    {
        string filePath = Combine(Directory.GetCurrentDirectory(), $"streams.{algorithm}."); // I like that interpolated string to assign the extension
        FileStream file = File.Create(filePath);

        Stream compressor;
        if (algorithm == "gzip")
        {
            compressor = new GZipStream(file, CompressionMode.Compress);
        }
        else
        {
            compressor = new BrotliStream(file, CompressionMode.Compress);
        }

        using (compressor)
        {
            using (XmlWriter xml = XmlWriter.Create(compressor))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("callsign");
                foreach (string item in Viper.CallSigns)
                {
                    xml.WriteElementString("callsign", item);
                }
            }
        } // Also closes the underlying stream

        OutputFileInfo(filePath);

        WriteLine($"Reading the compressed XML file {filePath}:");
        Stream decompressor;
        if (algorithm == "gzip")
        {
            decompressor = new GZipStream(file, CompressionMode.Decompress);
        }
        else
        {
            decompressor = new BrotliStream(file, CompressionMode.Decompress);
        }
        using (decompressor)
        using (XmlReader reader = XmlReader.Create(decompressor))

        while (reader.Read())
        {
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "callsign")) { reader.Read(); WriteLine($"{reader.Value}"); }
                /* 
                 * Alternative syntax:
                 * if(reader is {NodeType: XmlNodeType.Element, Name: "callsign"}){ reader.Read(); WriteLine($"{reader.Value}"); }
                 */
        }
    }
}