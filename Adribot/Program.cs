using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adribot
{
    class Program
    {
        static DiscordClient discord;
        static InteractivityModule interactivity;
        static CommandsNextModule commands;

        static async Task Main(string[] args) {
            // Load config.json
            string json = await GetConfigJson();
            var cfgjson = JsonConvert.DeserializeObject<ConfigJson>(json);

            // Setup DClient
            discord = new DiscordClient(new DiscordConfiguration {
                Token = cfgjson.Token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });

            // Attach Interactivity module
            interactivity = discord.UseInteractivity(new InteractivityConfiguration {
                PaginationBehaviour = TimeoutBehaviour.Ignore,
                PaginationTimeout = TimeSpan.FromMinutes(5),
                Timeout = TimeSpan.FromMinutes(1)
            });

            // Attach CommandsNext module
            commands = discord.UseCommandsNext(new CommandsNextConfiguration {
                StringPrefix = cfgjson.CommandPrefix
            });

            // Register Commands
            commands.RegisterCommands<Commands>();

            // Event handlers
            discord.MessageCreated += MessageCreated;

            await discord.ConnectAsync();

            // Infinite task
            await Task.Delay(-1);
        }

        static async Task MessageCreated(MessageCreateEventArgs e) {
            foreach(var user in e.MentionedUsers) {
                if(user.Id.Equals(135081249017430016) && e.Author.Id != 608275633218519060) {
                    await e.Message.RespondAsync($"Don't tag Adri, bad! {e.Author.Mention}\nhttps://imgur.com/F7WNiqc");
                }
            }
        }

        static async Task<string> GetConfigJson() {
            using(var fs = File.OpenRead("config.json")) {
                using(var sr = new StreamReader(fs, new UTF8Encoding(false))) {
                    return await sr.ReadToEndAsync();
                }
            }
        }

    }

    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("prefix")]
        public string CommandPrefix { get; private set; }
    }
}
