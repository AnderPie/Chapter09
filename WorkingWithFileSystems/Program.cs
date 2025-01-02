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
// Good practice: Check if a drive is ready before reading its properties. Removable drives will throw exceptions otherwise
#endregion

#region Managing directories
SectionTitle("Managing directories");
string newFolder = Combine(GetFolderPath(SpecialFolder.Personal), "NewFolder");
WriteLine($"Working with: {newFolder}");
// We must explicitly say which Exists method to use because we statically imported both Path and Directory
WriteLine($"Does it exist? {Path.Exists(newFolder)}");

WriteLine("Creating it...");
CreateDirectory(newFolder);
// Check again, it should exist now
WriteLine($"Does it exist? {Path.Exists(newFolder)}");

Write("Confirm the directory exists, then press any key.");
ReadKey(intercept: true);

WriteLine("Deleting it...");
Delete(newFolder, recursive: true);
// Hopefully not!
WriteLine($"Does it exist? {Path.Exists(newFolder)}");
#endregion

#region Managing files
SectionTitle("Managing files");

// Define a directory path to output files starting in the user's folder
string dir = Combine(GetFolderPath(SpecialFolder.Personal), "OutputFiles");

CreateDirectory(dir);

string textFile = Combine(dir, "Dummy.txt");
string backupFile = Combine(dir, "Dummy.bak");

WriteLine($"Working with: {textFile}");

WriteLine($"Does it exist? {File.Exists(textFile)}");

// create a new text file and write a line to it
StreamWriter textWriter = File.CreateText(textFile);
textWriter.WriteLine("Hello C#!");
textWriter.Close();

// Copy the file, overwriting it if it exists already
File.Copy(sourceFileName: textFile, destFileName: backupFile, overwrite: true);

WriteLine($"Does it exist? {File.Exists(textFile)}");

Write("Confirm that the file exists, then press any key");
ReadKey(intercept: true);

// Delete the file
File.Delete(textFile);
WriteLine($"Does it exist? {File.Exists(textFile)}");

// Read from the backup text file
WriteLine($"Reading contents of {backupFile}:");
StreamReader textReader = File.OpenText(backupFile);
WriteLine(textReader.ReadToEnd());
textReader.Close();
#endregion

#region Managing paths
SectionTitle("Managing paths");
WriteLine($"Folder Name: {GetDirectoryName(textFile)}");
WriteLine($"File Name: {GetFileName(textFile)}");
WriteLine($"File name without extension: {GetFileNameWithoutExtension(textFile)}");
WriteLine($"File extension: {GetExtension(textFile)}");
WriteLine($"Random File Name: {GetRandomFileName()}");
WriteLine($"Temporary File Name: {GetTempFileName()}");
#endregion

#region Getting file information
// To get more information about a file or directory (when it was last accessed and more) create an instance of the file info or directory info class

SectionTitle("Getting file information");

FileInfo info = new(backupFile);
WriteLine($"{backupFile}:");
WriteLine($"  Contains {info.Length} bytes.");
WriteLine($"  Last accessed: {info.LastAccessTime}");
WriteLine($"  Has readonly set to {info.IsReadOnly}");
#endregion

#region Controlling how you work with files

/* File.Open() has enum type overloads that specify how you want to use the file.
 *   FileMode:  CreateNew, OpenOrCreate, or Truncate
 *   FileAccess: ReadWrite, etc.
 *   FileShare: This control locks how other processes may access the file (ie Read)
 */

FileStream fileStream = File.Open(backupFile, FileMode.Open, FileAccess.Read, FileShare.Read);
fileStream.Close();

info = new(backupFile);
WriteLine($"Is the backup file compressed? {info.Attributes.HasFlag(FileAttributes.Compressed)}");
#endregion

#region Reading and writing with streams
// A stream is a sequence of bytes that can be read from and written to

/* 
 * There is an abstract Stream class and its many children who are concrete classes.
 * they include: FileStream, MemoryStream, BufferedStream, GzipStream, SslStream.
 * They all implement IDisposable as well, so they have a Dispose method for releasing unmanaged resources.
 * Common members for the Stream class include:
 * 
 * CanRead, CanWrite  | These properties let you know if you can read or write to the string
 * Length, Position   | These properties let you know the size of the stream, and the position within the stream. (exceptions can be thrown in some cases)
 * Close, Dispose     | This method closes the stream and releases its resources. You can call either, since Dispose actually calls Close()!
 * Flush              | If the stream has a buffer, then this method writes the bytes in the buffer to the stream, and the buffer is cleared.
 * CanSeek            | This property determines if the seek method can be used
 * Seek               | This method moves the current position to the one specified by the parameter
 * Read, ReadAsync    | This method reads a specified number of bytes from the stream into a byte array, and advances the position
 * Write, WriteAsync  | This method writes the contents of a byte array into the stream
 * ReadByte           | This method reads a byte and moves to the next byte
 * WriteByte          | This method writes a byte to the stream.
 */

/* 
 * Understanding storage streams
 * Some storage streams that represent a location where the bytes will be stored are described below:
 * Namespace           | Class           | Description
 * System.IO           | FileStream      | Bytes stored in the filesystem
 * System.IO           | MemoryStream    | Bytes stored in memory in the current process
 * System.Net.Sockets  | NetworkSTream   | Bytes stored at a network location
 */

/* 
 * Understanding function streams
 * Function streams can't exist on their own, but can only be "plugged into" other streams to add functionality.
 * Examples below:
 * Namespace                    | Class                     | Description
 * System.Security.Cryptography | CryptoStream              | This encrypts and decrypts the stream
 * System.IO.Compression        | GZipStream, DeflateStream | These compress and decompress the stream
 * System.Net.Security          | AuthenticatedStream       | Sends credentials across the stream
 */

/* 
 * Understanding stream helpers
 * Higher level objects to make working with streams easier. They implement IDisposable
 * Namespace  | Class                       | Description
 * System.IO  | StreamReader, StreamWriter  | Reads or writes from the underlying stream as plain text
 * System.IO  | BinaryReader, BinaryWriter  | This reads/writes from streams as .NET types, for example, .ReadDecimal() reads the next 16 bytes from the underlying stream as a decimal value
 * System.Xml | XmlReader, XmlWriter        | Reads/Write from the underlying stream using XML format
 */

/* 
 * It is very common to combine a helper like StreamWrite and multiple function streams like CryptoStream and GZipStream
 * Alongside a storage stream like FileStrean into a pipeline
 */

// When you open a file to read or write it, you use resources outside of .NET called 'unmanaged resources' that must be disposed of when ur done with them.


#endregion


#endregion