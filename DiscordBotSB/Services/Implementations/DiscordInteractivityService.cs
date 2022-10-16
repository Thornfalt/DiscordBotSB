using DiscordBotSB.Helpers;
using DiscordBotSB.Models;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBotSB.Services.Implementations
{
    public class DiscordInteractivityService : IDiscordInteractivityService
    {
        private readonly IApiService _apiService;
        private readonly ITextService _textService;
        private readonly IWatchlistService _watchlistService;

        public DiscordInteractivityService(IApiService apiService, ITextService textService, IWatchlistService watchlistService)
        {
            _apiService = apiService;
            _textService = textService;
            _watchlistService = watchlistService;
        }

        public async Task CreateInteractiveSearchMenu(CommandContext ctx, SearchResultObject boardgame)
        {
            Boardgame boardgameValues = new Boardgame();
            var options = new List<DiscordSelectComponentOption>();

            foreach (var item in boardgame.Items.FilterBoardgamesOnLanguage())
            {
                options.Add(new DiscordSelectComponentOption(item.Name, item.Id));
            }

            var dropdown = new DiscordSelectComponent("dropdown", null, options, false, 1, 1);

            var builder = new DiscordMessageBuilder().WithContent("Boardgames found :").AddComponents(dropdown);

            await builder.SendAsync(ctx.Channel);

            var test = new DiscordInteractionResponseBuilder();

            ctx.Client.ComponentInteractionCreated += async (s, e) =>
            {
                if (e.Id == "dropdown")
                {
                    System.Console.WriteLine(e.Id);
                    await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

                    boardgameValues = await _apiService.GetByIdApiRequestAsync(e.Values.GetValue(0) as string);
                    boardgameValues = boardgameValues.FilterBoardgameByStoreLocation();

                    var responseText = _textService.CreateStoreInformationText(boardgameValues);
                    await CreateResponseText(e, responseText, ctx);
                    e.Handled = true;
                    await e.Message.DeleteAsync("Clearing up messages");
                }
                else
                {
                    await HandleButtonPresses(e, ctx, boardgameValues);
                }
            };
        }

        private async Task HandleButtonPresses(ComponentInteractionCreateEventArgs e, CommandContext ctx, Boardgame boardgame)
        {
            if (e.Id == "yes_button")
            {
                var watchListResult = _watchlistService.AddToWatchlist(ctx, boardgame);
                e.Handled = true;
                await e.Message.DeleteAsync("Clearing up messages");
                await e.Channel.SendMessageAsync(watchListResult);
            }
            if (e.Id == "no_button")
            {
                e.Handled = true;
                await e.Message.DeleteAsync("Clearing up messages");
            }
        }

        private async Task CreateResponseText(ComponentInteractionCreateEventArgs e, string responseText, CommandContext ctx)
        {
            if (responseText.Contains("Out of stock"))
            {
                var yesButton = new DiscordButtonComponent(
                    ButtonStyle.Primary,
                    "yes_button",
                    "Yes",
                    false);
                var noButton = new DiscordButtonComponent(
                    ButtonStyle.Primary,
                    "no_button",
                    "No",
                    false);

                var buttonBuilder = new DiscordMessageBuilder().WithContent(responseText).AddComponents(new DiscordButtonComponent[]
                    {
                            yesButton,
                            noButton
                    });
                await buttonBuilder.SendAsync(ctx.Channel);
            }
            else
            {
                await e.Channel.SendMessageAsync(responseText);
            }
        }
    }
}
