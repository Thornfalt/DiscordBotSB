using DiscordBotSB.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiscordBotSB.Services.Implementations
{
    public class ApiService : IApiService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<Boardgame> GetByBoardGameGeekIdRequestAsync(string id)
        {
            var stringTask = await client.GetStringAsync($"https://bradspelspriser.se/api/info?eid={id}");
            BoardgameResultObject boardgame = JsonConvert.DeserializeObject<BoardgameResultObject>(stringTask);

            return boardgame.Boardgame;
        }

        public async Task<Boardgame> GetByIdApiRequestAsync(string id)
        {
            var stringTask = await client.GetStringAsync($"https://bradspelspriser.se/api/info?id={id}");
            BoardgameResultObject boardgame = JsonConvert.DeserializeObject<BoardgameResultObject>(stringTask);

            return boardgame.Boardgame;
        }

        public async Task<SearchResultObject> GetSearchRequestAsync(string input)
        {
            var stringTask = await client.GetStringAsync($"https://bradspelspriser.se/api/search?search={input}");
            SearchResultObject boardgames = JsonConvert.DeserializeObject<SearchResultObject>(stringTask);

            return boardgames;
        }
    }
}
