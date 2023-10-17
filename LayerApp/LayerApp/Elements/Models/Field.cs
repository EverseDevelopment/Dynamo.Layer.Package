using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    internal class Field
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public dynamic Value { get; set; }
        public List<Relation>? Related { get; set; }
    }
}
