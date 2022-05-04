using DiscordBotSB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotSB.Services
{
    public interface ITextService
    {
        string CreateStoreInformationText(Boardgame boardgame);
    }
}
