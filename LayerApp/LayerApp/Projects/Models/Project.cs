using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects
{
    internal class Project
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
    }
}
