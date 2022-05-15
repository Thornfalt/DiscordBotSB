using DiscordBotSB.Models;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotSB.Services
{
    public interface INotificationService
    {
        Task NotifyUsersAsync(DiscordClient discord);
    }
}
