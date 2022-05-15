using DiscordBotSB.Models;
using DiscordBotSB.Services;
using DiscordBotSB.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTests
{
    [TestClass]
    public class ApiServiceTests
    {
        private readonly ApiService apiService = new ApiService();

        [TestMethod]
        public async Task GetByBoardGameGeekIdRequestAsync_GetBoardgame_ReturnsBoardgame()
        {
            // Arrange
            const string BOARDGAMEGEEK_ID = "316554";
            // Act
            var game = await apiService.GetByBoardGameGeekIdRequestAsync(BOARDGAMEGEEK_ID);
            // Assert
            Assert.IsTrue(game != null && game.Items?.Count == 1);
        }

        [TestMethod]
        public async Task GetByIdApiRequestAsync_GetBoardgame_ReturnsBoardgame()
        {
            const string GAME_ID = "28903";

            var game = await apiService.GetByIdApiRequestAsync(GAME_ID);

            Assert.IsTrue(game != null && game.Items?.Count == 1);
        }

        [TestMethod]
        public async Task GetSearchRequestAsync_SearchBoardgames_ReturnsListOfBoardgames()
        {
            const string SEARCH_TEXT = "Dune";

            var games = await apiService.GetSearchRequestAsync(SEARCH_TEXT);

            Assert.IsTrue(games != null && games.Items.Any());
        }
    }
}
