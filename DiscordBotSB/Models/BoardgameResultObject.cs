using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordBotSB.Models
{
    public class BoardgameResultObject
    {
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "sitename")]
        public string Sitename { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "logo")]
        public string Logo { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<Boardgame> Items { get; set; }

        public Boardgame Boardgame => Items.FirstOrDefault();
    }
}
