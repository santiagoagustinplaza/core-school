using System;
using System.Collections.Generic;
using CoreSchool.Useful;

namespace CoreSchool.Entities
{
    public class Course : SchoolBaseObject, IPlace
    {
        public JourneyTypes Journey { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Student> Students { get; set; }
        public string Address { get; set; }

        public void CleanPlace()
        {
            Printer.DrawLine();
            Console.WriteLine("Cleaning Establishment...");
            Console.WriteLine($"{Name} course cleaned.");
        }
    }
}