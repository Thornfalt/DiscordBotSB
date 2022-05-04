using DiscordBotSB.Models;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;

namespace DiscordBotSB.Services
{
    public interface IWatchlistService
    {
        string AddToWatchlist(CommandContext ctx, Boardgame game);
    }
}
