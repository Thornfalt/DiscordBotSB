using DiscordBotSB.Models;
using DiscordBotSB.Services;
using DiscordBotSB.Services.Implementations;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotSB
{
    class Program
    {
        static DiscordClient discord;
        static CommandsNextExtension commands;

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
                .AddSingleton<IWatchlistService, WatchlistService>()
                .AddSingleton<INotificationService, NotificationService>()
                .BuildServiceProvider();

            (discord, commands) = DiscordConfig.Setup(serviceProvider, discord, commands);

            commands.RegisterCommands<Commands>();

            await discord.ConnectAsync();

            var notficationService = serviceProvider.GetService<INotificationService>();
            await notficationService.NotifyUsersAsync(discord);

            await Task.Delay(-1);
        }
    }
}
