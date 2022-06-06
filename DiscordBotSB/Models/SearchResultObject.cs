using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace DiscordBotSB.Models
{
    public class SearchResultObject
    {
        [JsonProperty(PropertyName = "items")]
        public List<Item> Items { get; set; }

        [JsonProperty(PropertyName = "count")]
        public string Count { get; set; }

        [JsonProperty(PropertyName = "pages")]
        public int Pages { get; set; }

        [JsonProperty(PropertyName = "per_page")]
        public int PerPage { get; set; }
    }

    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        [JsonProperty(PropertyName = "bestprice")]
        public double Bestprice { get; set; }

        [JsonProperty(PropertyName = "bestprice_ccy")]
        public string BestpriceCcy { get; set; }
    }
}
