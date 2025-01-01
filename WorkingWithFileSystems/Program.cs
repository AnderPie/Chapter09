using Spectre.Console; // To use Table.

#region Handling cross-platform environments and file systems

#region Pt 1
SectionTitle("Handling cross-platform environments and file systems");

// Create a spectre console table.
Table table = new();

// Add two columns with markup for colors
table.AddColumn("[blue]MEMBER[/]");
table.AddColumn("[red]VALUE[/]");

// Add rows
table.AddRow("Path.PathSeparator", PathSeparator.ToString());
table.AddRow("Path.DirectorySeparatorChar", DirectorySeparatorChar.ToString());
table.AddRow("Directory.GetCurrentDirectory()", GetCurrentDirectory());
table.AddRow("Environment.CurrentDirectory", CurrentDirectory);
table.AddRow("Environment.SystemDirectory", SystemDirectory);
table.AddRow("Path.GetTempPath()", GetTempPath());
table.AddRow("");
table.AddRow("GetFolderPath(SpecialFolder", "");
table.AddRow("    .System)", GetFolderPath(SpecialFolder.System));
table.AddRow("    .ApplicationData)", GetFolderPath(SpecialFolder.ApplicationData));
table.AddRow("    .MyDocuments", GetFolderPath(SpecialFolder.MyDocuments));
table.AddRow("    .Personal", GetFolderPath(SpecialFolder.Personal));

// Renter the table to the console
AnsiConsole.Write(table);

#endregion

#region Working with drives
/* 
 * Windows uses \ as the directory separator char, Linux and Mac use /.
 * DO NOT ASSUME WHAT SEPARATOR CHAR WILL BE USED BY YOUR PROGRAM!
 * instead use Path.DirectorySeparatorChar.
 */

SectionTitle("Managing drives");
Table drives = new();

drives.AddColumn("[blue]NAME[/]");
drives.AddColumn("[blue]TYPE[/]");
drives.AddColumn("[blue]FORMAT[/]");
drives.AddColumn(new TableColumn("[blue]SIZE (BYTES)[/]").RightAligned());
drives.AddColumn(new TableColumn("[blue]FREE SPACE[/]").RightAligned());

foreach(DriveInfo drive in DriveInfo.GetDrives())
{
    if (drive.IsReady)
    {
        drives.AddRow(drive.Name, drive.DriveType.ToString(), drive.DriveFormat, drive.TotalSize.ToString(), drive.AvailableFreeSpace.ToString("N0"));
    }
    else
    {
        drives.AddRow(drive.Name, drive.DriveType.ToString(), string.Empty, string.Empty, string.Empty);
    }
}

AnsiConsole.Write(drives);

#endregion
#endregion