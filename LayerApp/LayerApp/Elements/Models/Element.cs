using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Categories;
using Newtonsoft.Json;

namespace Elements
{
    internal class Element
    {
        [JsonProperty ("autoIncrementId")]
        public int AutoIncrementId { get; set; }

        [JsonProperty("category")]
        public CategoryBase Category { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("fields")]
        public Dictionary<string, Field>? Fields { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("params")]
        public Dictionary<string, Param>? Params { get; set; }

        [JsonProperty("spatialRelationships")]
        public List<SpatialRelationship>? SpatialRelationships { get; set; }

        [JsonProperty("starred")]
        public bool Starred { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("updatedBy")]
        public string UpdatedBy { get; set; }
    }
}
