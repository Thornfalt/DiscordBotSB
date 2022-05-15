using DiscordBotSB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotSB.Services
{
    public interface INotificationService
    {
        void NotifyUsers(Watchlist watchlist);
    }
}
