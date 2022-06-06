using DiscordBotSB.Models;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;

namespace DiscordBotSB.Services
{
    public interface IDiscordInteractivityService
    {
        Task CreateInteractiveSearchMenu(CommandContext ctx, SearchResultObject boardgame);
    }
}
