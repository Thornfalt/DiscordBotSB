using DiscordBotSB.Models;
using DiscordBotSB.Helpers;
using DSharpPlus;
using DSharpPlus.Entities;
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
                var member = await guild.GetMemberAsync(distinctUserId);
                var memberWatchlists = watchlists.Where(x => x.DiscordUserId == member.Id);

                var boardgames = await Task.WhenAll(memberWatchlists.Select(async x => await _apiService.GetByIdApiRequestAsync(x.BoardgameId)));

                if (boardgames.Any(x => x.HasBoardgameInStock()))
                {
                    var gamesInStock = boardgames.Select(x => x.FilterBoardgameByInStock()).ToList();
                    // TODO : Write the games in stock, and which stores that has the games in stock.

                    // TODO : Implement better text message using ITextService
                    await member.SendMessageAsync("Testing that games are in stock");
                }
            }
        }
    }
}
