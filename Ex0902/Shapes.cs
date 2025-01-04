using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Numerics;
using System.Xml.Serialization;

namespace Ex0902
{
    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    [XmlInclude(typeof(Square))]
    public class Shape
    {
        public String? Name {  get; set; }  
        public string? Color {  get; set; }
        public float Height { get; set; } = 0f;
        public float Width { get; set; } = 0f;

        public float Area {  get; set; } = 0f;

        public Shape() { }

        public Shape(string? color, float height, float width)
        {
            Color = color;
            Height = height;
            Width = width;
        }

    }

    public class Rectangle : Shape
    {
        public Rectangle() { }
        public Rectangle(string color, float height, float width)
        {
            Color = color;
            this.Name = "Rectangle";
            this.Height = height;
            this.Width = width;
            this.Area = height * width;
        }
    }

    public class Square : Rectangle
    {
        public Square()
        {
            Name = "Square";
        }
        public Square(float height)
        {
            this.Name = "Square";
            this.Height = height;
            this.Width = height;
            this.Area = height * height;
        }
        public Square(string color, float height, float width)
        {
            this.Name = "Square";
            // The way I'm handling the AreaException currently is sloppy.
            if (height != width)
            {
                throw new AreaException($"The height and width of a {Name} must be equal!");
            }
            this.Height = height;
            this.Width = width;
            this.Area = height * height;
            Color = color;
        }
    }

    
    public class Circle : Shape
    {
        float Diameter { get; set; }

        public Circle()
        {
            Name = "Circle";
        }
        public Circle(string color, float height, float width)
        {
            Name = "Circle";

            if (height != width)
            {
                throw new AreaException($"The height and width of a {Name} must be equal!");
            }
            float radius = height / 2f;
            Height = height;
            Width = width;
            Diameter = height;
            Area= float.Pi * radius * radius;
            Color = color;

        }

        public Circle(float radius)
        {
            Name = "Circle";
            Diameter = radius * 2f;
            Height = Diameter;
            Width = Diameter;
            Area = float.Pi * radius * radius;
        }
    }


    // The way I'm handling the AreaException currently is sloppy.
    public class AreaException : Exception
    {
        public AreaException() : base() { }
        public AreaException(string message) : base(message) { }
        public AreaException(string message, Exception innerException) : base(message, innerException) { }
    }
}
