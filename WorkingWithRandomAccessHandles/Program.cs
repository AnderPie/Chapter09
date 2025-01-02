using Microsoft.Win32.SafeHandles; // To use SafeFileHandle
using System.Text; // To use Encoding

// This region has the same name as our solution lmao
#region Reading and writing with random access handles

/* 
 * For the first 20 years of .NET's life, the only API to work directly with files was
 * the stream classes. These are great for working with data sequentially, but humans don't
 * really work that way. We tend to jump around all over the place.
 * There is a nice way to handle that introduced in .NET 6.
 */
using SafeFileHandle handle = File.OpenHandle(path: "coffee.txt", mode: FileMode.OpenOrCreate, access: FileAccess.ReadWrite); 

string message = "Coffee $9.99"; // Inflation is brutal
ReadOnlyMemory<byte> buffer = new(Encoding.UTF8.GetBytes(message));
await RandomAccess.WriteAsync(handle, buffer, fileOffset: 0);

// Read from the file by getting its length, allocating a memory buffer for its contents
// then reading the contents
long length = RandomAccess.GetLength(handle); 
Memory<byte> contentBytes = new(new byte[length]);
// ReadAsync(SafeFileHandle handle, sourceToWriteTo (Memory<byte> in this case), fileOffset: 0);
await RandomAccess.ReadAsync(handle, contentBytes, fileOffset: 0);
string content = Encoding.UTF8.GetString(contentBytes.ToArray());

WriteLine($"Content of file: {content}");
#endregion
