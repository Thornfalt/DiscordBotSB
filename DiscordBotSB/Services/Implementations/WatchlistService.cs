using DiscordBotSB.Models;
using DSharpPlus.CommandsNext;
using LiteDB;
using System;
using System.IO;
using System.Linq;

namespace DiscordBotSB.Services.Implementations
{
    public class WatchlistService : IWatchlistService
    {
        public string AddToWatchlist(CommandContext ctx, Boardgame game)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            using (var db = new LiteDatabase(projectDirectory + "\\MyDB.db"))
            {
                var col = db.GetCollection<Watchlist>("watchlist");

                var watchList = new Watchlist()
                {
                    BoardgameId = game.Items[0].Id,
                    DiscordUserId = ctx.User.Id
                };

                //Uncomment code for debugging results
                //var results = col.FindAll().ToList();

                if (col.Exists(x => x.BoardgameId == watchList.BoardgameId && x.DiscordUserId == watchList.DiscordUserId))
                {
                    return $"{game.Items[0].Name} already exists in your watchlist";
                }

                col.Insert(watchList);

                return $"Successfully added {game.Items[0].Name} to your watchlist";
            }
        }
    }
}
