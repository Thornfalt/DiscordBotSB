using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotSB
{
    public static class DiscordConfig
    {
        public static (DiscordClient, CommandsNextExtension) Setup(ServiceProvider services, DiscordClient discord, CommandsNextExtension commands)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddUserSecrets<Program>()
                .Build();

            // Gets ApiToken from user secrets
            var apiToken = configurationBuilder.GetChildren()
                    .Where(x => x.Key == "ApiToken")
                    .Select(x => x.Value)
                    .FirstOrDefault();

            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = apiToken,
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug,
                Intents = DiscordIntents.AllUnprivileged
            });

            discord.UseInteractivity(new InteractivityConfiguration()
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromSeconds(30)
            });

            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { "!" },
                EnableDms = true,
                Services = services
            });

            return (discord, commands);
        }
    }
}
