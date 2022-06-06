using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotSB.Models
{
    public class Versions
    {
        [JsonProperty(PropertyName = "lang")]
        public List<string> Lang { get; set; }

        [JsonProperty(PropertyName = "version")]
        public List<string> Version { get; set; }
    }
}
