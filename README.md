The following is work undertaken to learn C# through use of Mark Price's C#12 and .NET 8 Modern Cross-Platform Development Fundamentals.  
# Exercise 9
9.1 - Test your knowledge

## What is the difference between using the File class and the FileInfo class?
The File class, to paraphrase the comments in its definition, is a static class that aids in the manipulation 
individual files and assists in the creation of FileStream (System.IO.Filestream) objects. The FileInfo class is described
similarly to File in its own definition, however it inherits from the FileSystemInfo class. FileSystemInfo has a bunch of
public getter and/or setter properties which give you handy info about the file, such as when it was last written to, its extension,
etc.
## What is the difference between the ReadByte method and the Read method of a stream?
Maybe I am missing something, but the Stream class does not have a 'Read' method, only a ReadByte method.
The method ReadByte reads a bunch of bytes sequentially. This is very handy alongside serialization and deserialization 
helpers, which can encode .NET objects into the desired format (text, bit arrays, JSON, you name it) to 
be decoded (deserialized) at a later date.
## When would you use the StringReader, TextReader, and StreamReader classes?
TextReader is the base class of StringReader and StreamReader. As the names suggests stringreader reads characters from a string
 while streamreader reads characters from a stream. StringReader should be used when you are dealing with in-memory strings,
and you don't need an external source file. Meanwhile streamreader is great if reading or writing from files or reading from
networks, or when handling data too big to fit in one string. 

TL:DR, use StreamReader when dealing with external data streams, Stringreader when handling in-memory strings (that's what it is built
for), and you're using TextReader when you use either of them, because they are its children.
## What does the DeflateStream type do?
Used for compressing or decompressing streams with the Deflate algorithm. 
https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.deflatestream?view=net-8.0 
## How many bytes per character does UTF-8 encoding use?
One to four bytes. 8-32 bits is not bad!

##  What is an object graph?
An object graph is a serialized version of an in-memory object, encoded in some standardized format. 
A JSON encoded object graph might deliver bank account information that could be deserialized and brought into memory as
a List<BankAccount> object. 

## What is the best serialization format to choose to minimize space requirements?
Some sort of binary serialization would be most performant. I'll provide specifics after Googling it. 
-- POST GOOGLE (actually I use bing, does that make me weird) I don't use it as a verb tho, don't worry --
Protobuf would be a good option. 

## What is the best serialization format to choose for cross-platform compatibility?
JSON is highly ubiquitous, reasonably efficient in terms of size, and human readable to boot. 

## Why is it bad to use a string value like "\Code\Chapter01\ to represent a path, and what should you do instead?
The directory delimiter in path strings varies between operating systems. On Linux and Mac, for instance, we use / instead of \ .
it is better to use the 'Path.DirectorySeparatorChar' property instead.

## Where can you find information about NuGet packages and their dependencies?
Hmm, to answer this question in a cheeky way, Google. I'm going to google the answer to this question, actually :D 
--- Post 'Google' ---
In Powerhshell you would do something like
get-package -list 'packageName' | select dependencies

You could also google the package and figure out its dependencies and read about it
