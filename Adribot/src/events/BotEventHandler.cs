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
        public bool EnableMessageCreated;
        public bool EnableGuildCreated;

        public BotEventHandler(DiscordClient client) =>
            _client = client;


        public void Attach() {
            if(EnableMessageCreated)
                _client.MessageCreated += MessageCreatedAsync;
            if(EnableGuildCreated)
                _client.GuildCreated += GuildCreatedAsync;
        }

        private Task GuildCreatedAsync(DiscordClient sender, GuildCreateEventArgs e) {
            throw new NotImplementedException();
        }

        private async Task MessageCreatedAsync(DiscordClient sender, MessageCreateEventArgs e) {
            if(e.Message.MentionedUsers.Any(x => x.Id == 135081249017430016)) {
                await e.Message.CreateReactionAsync(DiscordEmoji.FromName(sender, ":meow_pinghiss:"));
            }
        }
    }
}
