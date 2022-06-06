using DiscordBotSB.Services;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace DiscordBotSB
{
    public class Commands : BaseCommandModule
    {
        private readonly IApiService _apiService;
        private readonly ITextService _textService;
        private readonly IDiscordInteractivityService _discordInteractivityService;
        private readonly IWatchlistService _watchlistService;

        public Commands(IApiService apiService, ITextService textService, IDiscordInteractivityService discordInteractivityService, IWatchlistService watchlistService)
        {
            _apiService = apiService;
            _textService = textService;
            _discordInteractivityService = discordInteractivityService;
            _watchlistService = watchlistService;
        }

        [Command("hi")]
        public async Task GreetingsCommand(CommandContext ctx)
        {
            await ctx.RespondAsync(string.Format("Hi, {0} !", ctx.User.Mention));
        }

        [Command("id")]
        public async Task GetGameByIdCommand(CommandContext ctx, [RemainingText] string id)
        {
            var boardgame = await _apiService.GetByBoardGameGeekIdRequestAsync(id);
            await ctx.RespondAsync(_textService.CreateStoreInformationText(boardgame));
        }

        [Command("search")]
        public async Task SearchGameCommand(CommandContext ctx, [RemainingText] string input)
        {
            var searchResult = await _apiService.GetSearchRequestAsync(input);
            await _discordInteractivityService.CreateInteractiveSearchMenu(ctx, searchResult);
        }

        [Command("add")]
        public async Task AddToWatchlistCommand(CommandContext ctx, [RemainingText] string input)
        {
            var boardgame = await _apiService.GetByBoardGameGeekIdRequestAsync(input);
            var result = _watchlistService.AddToWatchlist(ctx, boardgame);
            await ctx.RespondAsync(result);
        }

        [Command("remove")]
        public async Task RemoveFromWatchlistCommand(CommandContext ctx, [RemainingText] string input)
        {
            var boardgame = await _apiService.GetByBoardGameGeekIdRequestAsync(input);
            var result = _watchlistService.RemoveFromWatchlist(ctx, boardgame);
            await ctx.RespondAsync(result);
        }

        [Command("list")]
        public async Task ListBoardgamesInWatchlistCommand(CommandContext ctx)
        {
            var result = await _watchlistService.PrintUserBoardgamesFromWatchlist(ctx);
            await ctx.RespondAsync(result);
        }
    }
}
