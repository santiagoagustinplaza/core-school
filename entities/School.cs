using System;
using System.Collections.Generic;
using CoreSchool.Useful;

namespace CoreSchool.Entities
{
    public class School : SchoolBaseObject, IPlace
    {
        public int CreationYear { get; set; }

        public string Country { get; set; }
        public string City { get; set; }

        public string Address { get; set; }

        public SchoolTypes SchoolType { get; set; }
        public List<Course> Courses { get; set; }

        public School(string name, int year) => (Name, CreationYear) = (name, year);

        public School(string name, int year, SchoolTypes type, string country = "", string city = "") : base()
        {
            (Name, CreationYear) = (name, year);
            Country = country;
            City = city;
        }
        public override string ToString()
        {
            return $"Name: \"{Name}\", Type: {SchoolType} {System.Environment.NewLine} Country: {Country}, City:{City}.";
        }
        public void CleanPlace()
        {
            Printer.DrawLine();
            Console.WriteLine("Cleaning School...");

            foreach (var course in Courses)
            {
                course.CleanPlace();
            }
            Printer.WriteTitle($"{Name} school cleaned.");
            Printer.Beep(1000, amount: 3);
        }
    }
}