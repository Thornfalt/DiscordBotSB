using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordBotSB.Models
{
    public class Boardgame
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "thumbnail")]
        public string Thumbnail { get; set; }

        [JsonProperty(PropertyName = "external_id")]
        public string ExternalId { get; set; }

        [JsonProperty(PropertyName = "prices")]
        public List<Price> Prices { get; set; }

        public bool HasBoardgameInStock() => Prices.Any(x => x.Stock == "Y");
    }
}
