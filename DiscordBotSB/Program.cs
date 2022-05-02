using DiscordBotSB.Services;
using DiscordBotSB.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DiscordBotSB
{
    class Program
    {
        static void Main(string[] args)
        {
            // Required for DSharpPlus???
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IApiService, ApiService>()
                .AddSingleton<ITextService, TextService>()
                .AddSingleton<IDiscordInteractivityService, DiscordInteractivityService>()
                .BuildServiceProvider();

            await DiscordConfig.Setup(serviceProvider);

            await Task.Delay(-1);
        }
    }
}
