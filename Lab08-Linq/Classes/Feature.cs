using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Lab08_Linq.Classes
{
    public class Feature
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        public override string ToString()
        {
            return $"|{this.Properties.City}|{this.Properties.Borough}|{this.Properties.County}|" +
                   $"{this.Properties.Address}|{this.Properties.Zip}|{this.Properties.Neighborhood}|" +
                   $"{this.Properties.State}|";
        }

    }
}
