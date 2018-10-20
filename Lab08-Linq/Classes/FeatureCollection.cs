using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lab08_Linq.Classes
{
    public class FeatureCollection
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public List<Feature> Features { get; set; }
    }
}
