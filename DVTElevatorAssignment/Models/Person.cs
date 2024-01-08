using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int OriginFloor { get; set; }
        public int DestinationFloor { get; set; }
        public string? Direction { get; set; }
    }
}
