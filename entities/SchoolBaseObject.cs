using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSchool.Entities
{
    public abstract class SchoolBaseObject
    {
        public string UniqueId { get; private set; }
        public string Name { get; set; }

        public SchoolBaseObject()
        {
            UniqueId = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{Name},{UniqueId}";
        }
    }
}
