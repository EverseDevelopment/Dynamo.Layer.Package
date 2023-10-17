using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views
{
    internal class FilterConfig
    {
        public string Condition { get; set; }
        public List<string> Filters { get; set; } // Adjust this if 'filters' field in the response has a different structure
    }
}
