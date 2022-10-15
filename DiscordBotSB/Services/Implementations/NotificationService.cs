using DiscordBotSB.Helpers;
using DiscordBotSB.Models;
using DSharpPlus;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotSB.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        const ulong GUILD_ID = 608612282863190026;

        private readonly IApiService _apiService;
        private readonly ITextService _textService;

        public NotificationService(IApiService apiService, ITextService textService)
        {
            _apiService = apiService;
            _textService = textService;
        }

        public async Task NotifyUsersAsync(DiscordClient discord)
        {
            var watchlists = GetWatchlists();
            await NotifyUsers(discord, watchlists);
        }

        private List<Watchlist> GetWatchlists()
        {
            var watchlists = new List<Watchlist>();
            
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            using (var db = new LiteDatabase(projectDirectory + "\\MyDB.db"))
            {
                var collection = db.GetCollection<Watchlist>("watchlist");
                collection.EnsureIndex("watchlist");
                watchlists.AddRange(collection.FindAll().ToList());
            }

            return watchlists;
        }

        private async Task NotifyUsers(DiscordClient discord, List<Watchlist> watchlists)
        {
            // Test stuff
            var test = discord.Guilds;
            var guild = await discord.GetGuildAsync(GUILD_ID);

            foreach (var distinctUserId in watchlists.Select(x => x.DiscordUserId).Distinct())
            {
                string message = "Game : {0} is in stock at {1}";
                var stringBuilder = new StringBuilder();
                var member = await guild.GetMemberAsync(distinctUserId);
                var memberWatchlists = watchlists.Where(x => x.DiscordUserId == member.Id);

                var boardgames = await Task.WhenAll(memberWatchlists.Select(async x => await _apiService.GetByIdApiRequestAsync(x.BoardgameId)));

                if (boardgames.Any(x => x.HasBoardgameInStock()))
                {
                    var gamesInStock = FilterBoardgamesOnUserSettings(boardgames);
                    if (gamesInStock.Any())
                    {
                        foreach (var game in gamesInStock)
                        {
                            stringBuilder.AppendLine(string.Format(message, game.Name, game.Url));
                        }
                        await member.SendMessageAsync(stringBuilder.ToString());
                    }
                }
            }
        }

        private List<Boardgame> FilterBoardgamesOnUserSettings(Boardgame[] boardgames)
        {
            var gamesInStock = boardgames.Select(x => x.FilterBoardgameByInStock());
            gamesInStock = gamesInStock.Select(x => x.FilterBoardgameByStoreLocation());

            return gamesInStock.Where(x => x.Prices.Any()).ToList();
        }
    }
}
