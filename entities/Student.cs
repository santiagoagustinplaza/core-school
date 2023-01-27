using System;
using System.Collections.Generic;

namespace CoreSchool.Entities
{
    public class Student : SchoolBaseObject
    {
        public List<Test> Tests { get; set; } = new List<Test>();
    }
}
