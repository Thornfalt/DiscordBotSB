using DiscordBotSB.Models;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;

namespace DiscordBotSB.Services
{
    public interface IWatchlistService
    {
        string AddToWatchlist(CommandContext ctx, Boardgame game);
        string RemoveFromWatchlist(CommandContext ctx, Boardgame game);
        Task<string> PrintUserBoardgamesFromWatchlist(CommandContext ctx);
    }
}
