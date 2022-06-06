using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotSB.Models
{
    public class Watchlist
    {
        public ObjectId WatchlistId { get; set; }
        public ulong DiscordUserId { get; set; }
        public string BoardgameId { get; set; }
    }
}
