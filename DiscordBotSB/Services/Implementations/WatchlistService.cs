using DiscordBotSB.Models;
using DSharpPlus.CommandsNext;
using LiteDB;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotSB.Services.Implementations
{
    public class WatchlistService : IWatchlistService
    {
        private readonly IApiService _apiService;
        private readonly ITextService _textService;

        public WatchlistService(IApiService apiService, ITextService textService)
        {
            _apiService = apiService;
            _textService = textService;
        }

        public string AddToWatchlist(CommandContext ctx, Boardgame game)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            using (var db = new LiteDatabase(projectDirectory + "\\MyDB.db"))
            {
                var col = db.GetCollection<Watchlist>("watchlist");

                var watchList = new Watchlist()
                {
                    WatchlistId = ObjectId.NewObjectId(),
                    BoardgameId = game.Id,
                    DiscordUserId = ctx.User.Id
                };

                //Uncomment code for debugging results
                //var results = col.FindAll().ToList();

                if (col.Exists(x => x.BoardgameId == watchList.BoardgameId && x.DiscordUserId == watchList.DiscordUserId))
                {
                    return $"{game.Name} already exists in your watchlist";
                }

                col.Insert(watchList);

                col.EnsureIndex("watchlist");

                return $"Successfully added {game.Name} to your watchlist";
            }
        }

        public string RemoveFromWatchlist(CommandContext ctx, Boardgame game)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            using (var db = new LiteDatabase(projectDirectory + "\\MyDB.db"))
            {
                var col = db.GetCollection<Watchlist>("watchlist");

                if (!col.Exists(x => x.BoardgameId == game.Id && x.DiscordUserId == ctx.Member.Id))
                {
                    return $"{game.Name} does not exist in your watchlist";
                }

                var watchlist = col.FindOne(x => x.BoardgameId == game.Id && x.DiscordUserId == ctx.Member.Id);

                col.Delete(watchlist.WatchlistId);

                col.EnsureIndex("watchlist");

                return $"Successfully removed {game.Name} from your watchlist";
            }
        }

        public async Task<string> PrintUserBoardgamesFromWatchlist(CommandContext ctx)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            using (var db = new LiteDatabase(projectDirectory + "\\MyDB.db"))
            {
                var col = db.GetCollection<Watchlist>("watchlist");

                var userBoardgames = col.Find(x => x.DiscordUserId == ctx.Member.Id);

                var boardgames = await Task.WhenAll(userBoardgames.Select(async x => await _apiService.GetByIdApiRequestAsync(x.BoardgameId)));

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("The boardgames in your watchlist:");

                foreach (var boardgame in boardgames)
                {
                    stringBuilder.AppendLine(boardgame.Name);
                }

                return stringBuilder.ToString();
            }
        }
    }
}
