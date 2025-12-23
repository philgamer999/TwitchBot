using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    // Application settings model for deserializing appsettings.json
    public class AppSettings
    {
        // Twitch bot configuration settings
        public TwitchSettings Twitch { get; set; } = new TwitchSettings();
    }

    // Twitch bot specific settings
    public class TwitchSettings
    {
        public string BotUsername { get; set; } = string.Empty;
        public string OAuthToken { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;
    }
}
