using Adribot.src.entities;
using Adribot.src.entities.source;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Adribot.src.events
{
    class BotEventHandler
    {
        private DiscordClient _client;
        private DBController _DBController = new();

        public bool EnableMessageCreated;
        public bool EnableGuildCreated;
        public bool EnableGuildDownloadCompleted;

        public BotEventHandler(DiscordClient client) =>
            _client = client;


        public void Attach() {
            if(EnableMessageCreated)
                _client.MessageCreated += MessageCreatedAsync;
            if(EnableGuildCreated)
                _client.GuildCreated += GuildCreatedAsync;
            if(EnableGuildDownloadCompleted)
                _client.GuildDownloadCompleted += GuildDownloadCompletedAsync;
        }

        private Task GuildDownloadCompletedAsync(DiscordClient sender, GuildDownloadCompletedEventArgs e) {
            throw new NotImplementedException();
        }

        private Task GuildCreatedAsync(DiscordClient sender, GuildCreateEventArgs e) {
            throw new NotImplementedException();
        }

        private async Task MessageCreatedAsync(DiscordClient sender, MessageCreateEventArgs e) {
            if(e.Message.MentionedUsers.Any(x => x.Id == 135081249017430016)) {
                await e.Message.CreateReactionAsync(DiscordEmoji.FromName(sender, ":meow_pinghiss:"));
            }
        }

        private async Task AddNewGuild(DiscordGuild guild) {
            await _DBController.AddAsync(new Guild {
                GuildId = guild.Id
            });
        }
    }
}
