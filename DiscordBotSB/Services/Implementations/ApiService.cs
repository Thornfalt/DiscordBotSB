using DiscordBotSB.Models;
using DiscordBotSB.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotSB.Services.Implementations
{
    public class ApiService : IApiService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<Boardgame> GetByBoardGameGeekIdRequestAsync(string id)
        {
            var stringTask = await client.GetStringAsync($"https://bradspelspriser.se/api/info?eid={id}");
            Boardgame boardgame = JsonConvert.DeserializeObject<Boardgame>(stringTask);

            return boardgame;
        }

        public async Task<Boardgame> GetByIdApiRequestAsync(string id)
        {
            var stringTask = await client.GetStringAsync($"https://bradspelspriser.se/api/info?id={id}");
            Boardgame boardgame = JsonConvert.DeserializeObject<Boardgame>(stringTask);

            return boardgame;
        }

        public async Task<Boardgame> GetSearchRequestAsync(string input)
        {
            var stringTask = await client.GetStringAsync($"https://bradspelspriser.se/api/search?search={input}");
            Boardgame boardgames = JsonConvert.DeserializeObject<Boardgame>(stringTask);

            return boardgames;
        }
    }
}
