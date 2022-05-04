using DiscordBotSB.Models;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscordBotSB.Services.Implementations
{
    public class DiscordInteractivityService : IDiscordInteractivityService
    {
        private readonly IApiService _apiService;
        private readonly ITextService _textService;

        public DiscordInteractivityService(IApiService apiService, ITextService textService)
        {
            _apiService = apiService;
            _textService = textService;
        }

        public async Task CreateInteractiveSearchMenu(CommandContext ctx, Boardgame boardgame)
        {
            var options = new List<DiscordSelectComponentOption>();

            foreach (var item in boardgame.Items)
            {
                options.Add(new DiscordSelectComponentOption(item.Name, item.Id));
            }

            var dropdown = new DiscordSelectComponent("dropdown", null, options, false, 1, 1);

            var builder = new DiscordMessageBuilder().WithContent("Boardgames found :").AddComponents(dropdown);

            await builder.SendAsync(ctx.Channel);

            ctx.Client.ComponentInteractionCreated += async (s, e) =>
            {
                await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

                var boardgameValues = await _apiService.GetByIdApiRequestAsync(e.Values.GetValue(0) as string);
                // TODO : IMPROVE THIS!
                var responseText = _textService.CreateStoreInformationText(boardgameValues);
                await e.Channel.SendMessageAsync(responseText);
            };
        }
    }
}
