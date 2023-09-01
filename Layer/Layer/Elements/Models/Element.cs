using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Categories;

namespace Elements
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    internal class Element
    {
        public int AutoIncrementId { get; set; }
        public CategoryBase Category { get; set; }
        public bool Completed { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public Dictionary<string, Field>? Fields { get; set; }
        public string Id { get; set; }
        public string ModelRevitId { get; set; }
        public string Name { get; set; }
        public Dictionary<string, Param>? Params { get; set; }
        public List<SpatialRelationship>? SpatialRelationships { get; set; }
        public bool Starred { get; set; }
        public string Status { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
