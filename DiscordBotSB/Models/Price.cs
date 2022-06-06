using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotSB.Models
{
    public class Price
    {
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        [JsonProperty(PropertyName = "price")]
        public double ItemPrice { get; set; }

        [JsonProperty(PropertyName = "product")]
        public double Product { get; set; }

        [JsonProperty(PropertyName = "fee")]
        public int Fee { get; set; }

        [JsonProperty(PropertyName = "stock")]
        public string Stock { get; set; }

        [JsonProperty(PropertyName = "shipping_known")]
        public bool ShippingKnown { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }
}
