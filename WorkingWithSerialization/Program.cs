using System.Reflection.PortableExecutable;
using System.Xml.Serialization; // To use the XML serializer
using Microsoft.VisualBasic;
using Packt.Shared; // Use the person class
using Newtonsoft; // To practice deserializing JSON files
using FastJson = System.Text.Json.JsonSerializer;

#region Serializing objects as XML
List<Person> people = new()
{
    new(initialSalary: 30_000M)
    {
        FirstName = "Alice",
        LastName = "Smith",
        DateOfBirth = new(year: 1974, month: 3, day: 14),
    },
    new(initialSalary: 43_000M)
    {
        FirstName = "Jane",
        LastName = "Doe",
        DateOfBirth = new(year: 1974, month: 3, day: 14),
    },
    new(initialSalary: 65_200M)
    {
        FirstName = "Margwa",
        LastName = "Llarynx",
        DateOfBirth = new(year: 1974, month: 3, day: 14),
        Children = new()
        {
            new(initialSalary: 94_430M)
            {
                FirstName = "Jeffrey",
                LastName = "Vlads",
                DateOfBirth = new(year: 1974, month: 3, day: 14)
            }
        }
    }
};

SectionTitle("Serializing as XML");

// Create serializer to format a "List of Person" as XML

XmlSerializer xs = new(type: people.GetType());

// Create a file to write to
string path = Combine(CurrentDirectory, "people.xml");

using (FileStream stream = File.Create(path))
{
    // Serialize the object graph to the stream
    xs.Serialize(stream, people);
} // Using completes, automatically closing the stream and releasing unmanaged resources

OutputFileInfo(path);
#endregion
#region Deserializing XML as objects
SectionTitle("Deserializing XML files");
using(FileStream xmlLoad = File.Open(path, FileMode.Open))
{
    // Deserialize and cast the object graph into a "List of Person
    List<Person>? loadedPeople = xs.Deserialize(xmlLoad) as List<Person>;

    if(loadedPeople is not null)
    {
        foreach(Person p in loadedPeople)
        {
            WriteLine($"{p.LastName} has {p.Children?.Count()} children.");
        }
    }
}
#endregion
#region Serializing with JSON

SectionTitle("Serializing with JSON");

// Create a file to write to
string jsonPath = Combine(CurrentDirectory, "people.json");
using (StreamWriter jsonStream = File.CreateText(jsonPath)){
    Newtonsoft.Json.JsonSerializer jss = new();

    // Serialize the object graph into a string
    jss.Serialize(jsonStream, people);
}
OutputFileInfo(jsonPath);

#region Working with modern JSON APIs
//System.Text.Json.JsonSerializer is very fast and performant
//It reads and writes JSON using UTF-8 rather than 16.

SectionTitle("Deserializing JSON files");
await using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
{
    // Deserialize object graph into a List of Person
    List<Person>? loadedPeople = await FastJson.DeserializeAsync(utf8Json: jsonLoad, returnType: typeof(List<Person>)) as List<Person>;

    if (loadedPeople is not null)
    {
        foreach(Person p in loadedPeople)
        {
            WriteLine($"{p.LastName} has {p.Children?.Count ?? 0} children.");
        }
    }
}
#endregion
#endregion


