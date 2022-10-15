using DiscordBotSB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscordBotSB.Helpers
{
    public static class BoardgameHelpers
    {
        private const string LANG_SWEDISH = "lang:SE";
        private const string LANG_ENGLISH = "lang:GB";
        private const string STORE_SWEDEN = "SE";
        private const string STORE_DENMARK = "DK";
        private const string STORE_NORWAY = "NO";
        private const string STORE_FINLAND = "FI";

        /// <summary>
        /// Returns list with boardgames in chosen language (english and swedish).
        /// </summary>
        public static List<Item> FilterBoardgamesOnLanguage(this List<Item> items)
        {
            var filteredBoardgames = new List<Item>();
            foreach (var item in items)
            {
                // Certain games have Version as null or empty but game is still in english
                if (string.IsNullOrWhiteSpace(item.Version))
                {
                    filteredBoardgames.Add(item);
                    continue;
                }

                if (item.Version.Contains(LANG_SWEDISH))
                {
                    filteredBoardgames.Add(item);
                    continue;
                }

                if (item.Version.Contains(LANG_ENGLISH))
                {
                    filteredBoardgames.Add(item);
                    continue;
                }
            }

            return filteredBoardgames;
        }

        /// <summary>
        /// Removes all stores that does not have the game in stock.
        /// </summary>
        public static Boardgame FilterBoardgameByInStock(this Boardgame boardgame)
        {
            boardgame.Prices.RemoveAll(x => x.Stock != "Y");
            return boardgame;
        }

        /// <summary>
        /// Removes all stores that are not located in user configured countries
        /// </summary>
        public static Boardgame FilterBoardgameByStoreLocation(this Boardgame boardgame)
        {
            string[] accpetedStores = { STORE_SWEDEN, STORE_NORWAY, STORE_FINLAND, STORE_DENMARK };

            boardgame.Prices
                .RemoveAll(x => !accpetedStores.Contains(x.Country));

            return boardgame;
        }
    }
}
