using DiscordBotSB.Models;
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
        const ulong MEMBER_ID = 369871247380578315;

        private readonly IApiService _apiService;
        private readonly ITextService _textService;

        public NotificationService(IApiService apiService, ITextService textService)
        {
            _apiService = apiService;
            _textService = textService;
        }

        public async Task NotifyUsersAsync(DiscordClient discord)
        {
            List<Watchlist> listOfGames = new List<Watchlist>();
            var guild = await discord.GetGuildAsync(GUILD_ID);

            var member = await guild.GetMemberAsync(369871247380578315);

            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            using (var db = new LiteDatabase(projectDirectory + "\\MyDB.db"))
            {
                var collection = db.GetCollection<Watchlist>("watchlist");
                listOfGames = collection.Find(x => x.DiscordUserId == MEMBER_ID).ToList();
            }

            var boardgames = await Task.WhenAll(listOfGames.Select(async x => await _apiService.GetByIdApiRequestAsync(x.BoardgameId)));

            var gamesInStock = GetBoardgamesInStock(boardgames);

            if (gamesInStock.Any())
            {
                // TODO : Implement better text message using ITextService
                await member.SendMessageAsync("Testing that games are in stock");
            }
            
        }

        private Boardgame[] GetBoardgamesInStock(Boardgame[] boardgames)
        {
            // It works but it hurts my eyes to look at
            return boardgames
                .Where(x => x.Items
                    .Where(x => x.Prices
                        .Where(x => x.Stock == "Y")
                        .Any())
                    .Any())
                .ToArray();
        }
    }
}
