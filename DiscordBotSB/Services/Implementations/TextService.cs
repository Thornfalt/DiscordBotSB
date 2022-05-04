using DiscordBotSB.Models;
using System.Linq;
using System.Text;

namespace DiscordBotSB.Services.Implementations
{
    public class TextService : ITextService
    {
        public string CreateStoreInformationText(Boardgame boardgame)
        {
            var prices = boardgame.Items
                .Select(x => x.Prices
                .OrderBy(x => x.Price))
                .FirstOrDefault();

            if (prices.Any(x => x.Stock == "Y"))
            {
                var stringBuilder = new StringBuilder();
                var stockedStores = prices.Where(x => x.Stock == "Y").ToList();

                stringBuilder.AppendLine(boardgame.Items[0].Name);

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
