using DiscordBotSB.Models;
using DiscordBotSB.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DiscordBotSBTests
{
    [TestClass]
    public class TextServiceTests
    {
        const string OUT_OF_STOCK_TEXT = "Out of stock";

        public Boardgame boardgameInStock =
            new Boardgame
            {
                Id = "139030",
                Name = "Mascarade",
                Prices = new List<Price>
                {
                    new Price
                    {
                        Stock = "Y",
                        Country = "GB"
                    }
                }
            };

        public Boardgame boardgameOutOfStock =
            new Boardgame
            {
                Id = "316554",
                Name = "Dune: Imperium",
                Prices = new List<Price>
                {
                    new Price
                    {
                        Stock = "N",
                        Country = "US"
                    }
                }
            };

        private readonly TextService textService = new TextService();

        [TestMethod]
        public void CreateStoreInformationText_GamesInStock_ReturnsStoreInformation()
        {
            var result = textService.CreateStoreInformationText(boardgameInStock);

            Assert.AreNotEqual(OUT_OF_STOCK_TEXT, result);
        }

        [TestMethod]
        public void CreateStoreInformationText_GamesOutOfStock_ReturnsOutOfStockText()
        {
            var result = textService.CreateStoreInformationText(boardgameOutOfStock);

            Assert.AreEqual(OUT_OF_STOCK_TEXT, result);
        }
    }
}
