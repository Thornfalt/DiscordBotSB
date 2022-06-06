using DiscordBotSB.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
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
            Assert.IsTrue(game != null && game.ExternalId == BOARDGAMEGEEK_ID);
        }

        [TestMethod]
        public async Task GetByIdApiRequestAsync_GetBoardgame_ReturnsBoardgame()
        {
            const string GAME_ID = "28903";

            var game = await apiService.GetByIdApiRequestAsync(GAME_ID);

            Assert.IsTrue(game != null && game.Id == GAME_ID);
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
