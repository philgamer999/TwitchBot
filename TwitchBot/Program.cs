using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;

namespace TwitchBot
{
    // https://twitchtokengenerator.com/

    internal class Program
    {
        // Twitch Bot Credentials
        static TwitchSettings twitchSettings = new TwitchSettings
        {
            BotUsername = "",
            OAuthToken = "",
            Channel = ""
        };

        // Twitch Client
        static TwitchClient client = null;

        // UI Controllers and Modules
        static BotController Controller = null;
        static ChatFish ChatFishModule = null;

        // Threads
        static Thread WindowThread;
        static Thread AppThread;

        static void Main(string[] args)
        {
            // Load settings from appsettings.json
            var loader = new AppSettingsLoader();
            var settings = loader.Load();
            twitchSettings = settings.Twitch;

            Console.WriteLine($"Loaded settings from 'appsettings.json'");
            Console.WriteLine($"Loaded settings for bot: {twitchSettings.BotUsername} in channel: {twitchSettings.Channel}");

            // Startup bot controller window thread
            WindowThread = new Thread(new ThreadStart(() =>
            {
                // Define bot controller and its command handler
                Controller = new BotController();
                Controller.OnCommand += cmd =>
                {
                    Console.WriteLine($"Controller Command: {cmd}");
                    switch (cmd) // handle commands from the controller UI in switch-case
                    {
                        case "TOGGLE":
                            BotRegistry();
                            break;
                        case "CHATFISH":
                            Console.WriteLine("Chat Fish Command Triggered");
                            ChatFish();
                            break;
                        default:
                            break;
                    }
                };
                // Start the controller application loop
                Application.Run(Controller);
            }));

            // Set the thread to STA and start it
            WindowThread.SetApartmentState(ApartmentState.STA);
            WindowThread.Start();

            // Keep the main thread alive
            Console.ReadLine();
        }


        // Event handler for received messages
        static void OnMessage(object sender, OnMessageReceivedArgs e)
        {
            Console.WriteLine($"{e.ChatMessage.Username}: {e.ChatMessage.Message}");
            ComputeMessage(e.ChatMessage.Username, e.ChatMessage.Message);
        }

        // Process the received message and execute commands
        private static void ComputeMessage(string user, string message)
        {
            // Test command for debugging
            if (message.Contains("test"))
            {
                Console.WriteLine($"{user} said test");
            }

            #region ChatFish Commands
            // ChatFish: Feed
            if (message.Contains("!feed"))
            {
                Console.WriteLine($"{user} spawned food!");
                if (ChatFishModule == null)
                    return;
                ChatFishModule.Feed();
            }
            // ChatFish: Move Up
            if (message.Contains("!up"))
            {
                Console.WriteLine($"{user} moved fish up!");
                if (ChatFishModule == null)
                    return;
                ChatFishModule.Up();
            }
            // ChatFish: Move Down
            if (message.Contains("!down"))
            {
                Console.WriteLine($"{user} moved fish down!");
                if (ChatFishModule == null)
                    return;
                ChatFishModule.Down();
            }
            // ChatFish: Move Right
            if (message.Contains("!right"))
            {
                Console.WriteLine($"{user} turned fish right!");
                if (ChatFishModule == null)
                    return;
                ChatFishModule.Right();
            }
            // ChatFish: Move Left
            if (message.Contains("!left"))
            {
                Console.WriteLine($"{user} turned fish left!");
                if (ChatFishModule == null)
                    return;
                ChatFishModule.Left();
            }
            #endregion

            // Add more commands here as needed
        }

        // Register or unregister the bot based on its current state
        static void BotRegistry()
        {
            if (IsBotConnected())
            {
                UnregisterBot();
            }
            else
            {
                RegisterBot();
            }
        }

        // Register the Twitch bot and connect to the channel
        static void RegisterBot()
        {
            var credentials = new ConnectionCredentials(
            $"{twitchSettings.BotUsername}",
            $"oauth:{twitchSettings.OAuthToken}"
            );

            client = new TwitchClient();
            client.Initialize(credentials, $"{twitchSettings.Channel}");

            client.OnMessageReceived += OnMessage;
            client.Connect();

            Console.WriteLine("Bot Started");
        }

        // Unregister the Twitch bot and disconnect from the channel
        static void UnregisterBot()
        {
            client.Disconnect();

            Console.WriteLine("Bot Stopped");
        }

        // Toggle the ChatFish module UI
        static void ChatFish()
        {
            if (AppThread == null || !AppThread.IsAlive)
            {
                AppThread = new Thread(() =>
                {
                    ChatFishModule = new ChatFish();
                    Application.Run(ChatFishModule);
                });

                AppThread.SetApartmentState(ApartmentState.STA);
                AppThread.Start();
            }
            else
            {
                // UI-thread-sicher schließen
                ChatFishModule.Invoke(new Action(() =>
                {
                    ChatFishModule.Close();
                }));

                AppThread = null;
            }
        }

        // Send a message to the Twitch chat
        static void SendMessage(string message)
        {
            if (client != null && client.IsConnected)
            {
                client.SendMessage(twitchSettings.Channel, message);
            }
        }

        // Check if the bot is currently connected
        static bool IsBotConnected()
        {
            return client != null && client.IsConnected;
        }

        // Clean up resources on application exit
        static void OnApplicationExit(object sender, EventArgs e)
        {
            UnregisterBot();
            if (ChatFishModule != null)
            {
                ChatFishModule.Invoke(new Action(() =>
                {
                    ChatFishModule.Close();
                }));
            }
            if (WindowThread.IsAlive) { WindowThread?.Abort(); }
            if (AppThread.IsAlive) { AppThread?.Abort(); }
        }
    }
}
