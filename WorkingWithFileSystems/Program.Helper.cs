using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// null namespace to merge with auto-generated program
partial class Program
{
    private static void SectionTitle(string title)
    {
        WriteLine();
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow; // High contrast 
        WriteLine($"*** {title} ***");
        ForegroundColor = previousColor;
    }
}