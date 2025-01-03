using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization; // To use [JsonInclude]

#region Controlling JSON processing
namespace Packt.Shared
{
    internal class Book
    {
        //Constructor to set non-nullable property
        public Book(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
        public string? Author { get; set; }

        // Fields
        [JsonInclude]  //Include this field
        public DateTime PublishDate;

        [JsonInclude]
        public DateTimeOffset Created;

        public ushort Pages;
    }
}
#endregion
