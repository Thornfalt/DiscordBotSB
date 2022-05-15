using DiscordBotSB.Models;
using DSharpPlus.CommandsNext;
using LiteDB;
using System;
using System.IO;

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

                // TODO : FIX THIS
                if (col.Exists(x => x == watchList))
                {
                    return $"{game.Items[0].Name} already exists in your watchlist";
                }

                col.Insert(watchList);

                // Uncomment code for debugging results
                //var results = col.Find(x => x.DiscordUserId == ctx.User.Id).ToList();

                return $"Successfully added {game.Items[0].Name} to your watchlist";
            }
        }
    }
}
