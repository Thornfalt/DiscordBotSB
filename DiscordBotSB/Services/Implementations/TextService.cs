using DiscordBotSB.Models;
using DiscordBotSB.Helpers;
using System.Linq;
using System.Text;

namespace DiscordBotSB.Services.Implementations
{
    public class TextService : ITextService
    {
        public string CreateStoreInformationText(Boardgame boardgame)
        {
            boardgame = boardgame.FilterBoardgameByInStock();

            if (boardgame.HasBoardgameInStock())
            {
                var stringBuilder = new StringBuilder();
                var stockedStores = boardgame.Prices.Where(x => x.Stock == "Y").ToList();

                stringBuilder.AppendLine(boardgame.Name);

                foreach (var stockedStore in stockedStores)
                {
                    stringBuilder.AppendLine($"--- {stockedStore.Country} --- {stockedStore.Link}");
                }

                return stringBuilder.ToString();
            }

            return "Out of stock";
        }
    }
}
