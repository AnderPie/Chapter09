using Ex0902;
using System.Xml.Serialization; // To use Shape and its children
/* 
 *    User Story
 * (done!) Define a bunch of Shape objects 
 *      Each specific shape should inherit from Shape
 *      Each Shape has a color, height, width, and area
 *      
 * (done!) Instantiate at least four shapes of at least 3 types (keeping it ez, rectangle, square, circle) 
 * 
 * Write the Shape Objects to an XML File
 * 
 * Read the XML file to instantiate a new list
 * 
 * Cannibalize code from former projects to expedite work, BUT remember that practice makes perfect
 *  Ultimately I cannabilized a little more than I should have, but so long as I spend the next ~1.5 hrs
 *  coding, no harm was done.
 * 
 * 
 */
#region Instantiate the shapes
List<Shape> shapes = new List<Shape>()
{
    new Circle(color: "Purple", 5, 5),
    new Square(color: "Pink", 4, 4),
    new Rectangle(color: "Creme", 4, 11), //Haha I should have made the rectangle square just to be silly
    new Circle(color: "Green", 3, 3)
};

SectionTitle("Our shapes prior to serialization");

foreach(Shape shape in shapes)
{
    WriteLine($"{shape.Name} is this color: {shape.Color}.");
    WriteLine($"Area, width, height: {shape.Area}. {shape.Width}, {shape.Height}.");
}
#endregion

XmlSerializer serializer = new(type: shapes.GetType());
#region Serialize the shapes
string path = Combine(CurrentDirectory, "shapes.xml");
using (FileStream stream = File.Create(path))
{    
    serializer.Serialize(stream, shapes);
};

OutputFileInfo(path);
SectionTitle("Magic has happened. There is now an XML file on your computer with my shapes. Spooky.");
#endregion

#region Deserialize the shapes.xml as a new list, and print that list
SectionTitle("Deserialize XML File");
using (FileStream loadSomethingXMLFlavored = File.Open(path, FileMode.Open) ){
    List<Shape>? shapesTwo = serializer.Deserialize(loadSomethingXMLFlavored) as List<Shape>;

    if (shapesTwo is not null)
    {
        foreach (Shape shape in shapesTwo)
        {
            WriteLine($"{shape.Name} is this color: {shape.Color}.");
            WriteLine($"Area, width, height: {shape.Area}. {shape.Width}, {shape.Height}.");
        }
    }
}
// See, EZ :) 

// (I definitely didn't lookup a boatload of syntax ;) ;) )
#endregion


