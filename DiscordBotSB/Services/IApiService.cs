using DiscordBotSB.Models;
using System.Threading.Tasks;

namespace DiscordBotSB.Services
{
    public interface IApiService
    {
        Task<Boardgame> GetByBoardGameGeekIdRequestAsync(string id);
        Task<Boardgame> GetByIdApiRequestAsync(string id);
        Task<SearchResultObject> GetSearchRequestAsync(string input);
    }
}
