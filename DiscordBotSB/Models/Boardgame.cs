using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace DiscordBotSB.Models
{
    public class Boardgame
    {
        [JsonProperty(PropertyName = "items")]
        public List<Items> Items { get; set; }
    }

    public class Items
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "prices")]
        public List<Prices> Prices { get; set; }
    }

    public class Prices
    {
        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "stock")]
        public string Stock { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }
}
