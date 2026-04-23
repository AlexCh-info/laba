using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1 
{
    public class Photo
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime DateTaken { get; set; }

        public Photo(string path, string description, DateTime dateTaken)
        {
            Path = path;
            Description = description;
            DateTaken = dateTaken;
        }

        public override string ToString()
        {
            return $"{Path} - {Description} ({DateTaken.ToString("dd.MM.yyyy")}";
        }
    }
}