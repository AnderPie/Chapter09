using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Null namespace extends auto-generated program.cs file

/*
 * Assists in completion of the Ex0209 Shapes user story described in Program.cs
 * 
 *
 *
 *
 *
 */
partial class Program
{
    // Print a pretty title for a section. I kind of want more asterisks tho
    private static void SectionTitle(string title)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"***** {title} *****");
        ForegroundColor = previousColor;
    }

    // Converts dictionary to table to print with help from Spectra.Console
    private static void DictionaryToTable(IDictionary dictionary)
    {
        Table table = new();
        table.AddColumn("Key");
        table.AddColumn("Value");
        foreach (string key in dictionary.Keys)
        {
            table.AddRow(key, dictionary[key]!.ToString()!);
        }

        AnsiConsole.Write(table);
    }

    // Prints pretty FileInfo data including size, name, directory
    private static void OutputFileInfo(string path)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine("**** File Info ****");
        ForegroundColor = previousColor;
        WriteLine($"File: {GetFileName(path)}");
        WriteLine($"Path: {GetDirectoryName(path)}");
        WriteLine($"Size: {new FileInfo(path).Length:N0} bytes");
        WriteLine("/--------------------------------");
        WriteLine(File.ReadAllText(path));
        WriteLine("--------------------------------/");

    }
}