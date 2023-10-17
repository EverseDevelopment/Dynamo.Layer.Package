using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Categories
{
    internal class Category : CategoryBase
    {
        public DateTime? CreatedAt { get; set; }
        public int Count { get; set; }
        public string Initials { get; set; }
        public string Instance { get; set; }
        public bool ModelCategory { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Order { get; set; } // Order property can be null hence marked as nullable
    }
}
