using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views
{
    internal class ViewDetail
    {
        public DateTime CreatedAt { get; set; }
        public List<FieldDisplay> FieldDisplay { get; set; }
        public FilterConfig FilterConfig { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<string> Sort { get; set; } // Adjust this if 'sort' field in the response has a different structure
        public DateTime UpdatedAt { get; set; }
    }
}
