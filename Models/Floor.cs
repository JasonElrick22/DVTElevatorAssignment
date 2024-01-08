using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Floor
    {
        public int FloorId { get; set; }
        public IList<Person> PersonsOnFloor { get; set; }
    }
}
