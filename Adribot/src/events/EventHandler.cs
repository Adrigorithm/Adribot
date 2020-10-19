using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;

namespace Adribot.events
{
    public class EventHandler {
        private readonly DiscordClient _client;

        public bool EnableMessageCreated { get; set; } = true;
        public bool EnableGuildMemberAdded { get; set; } = true;
        public bool EnableGuildBanAdded { get; set; } = true;

        public EventHandler(DiscordClient client) {
            _client = client;
            EnableEvents();
        }

        private void EnableEvents() {
            if(EnableMessageCreated) {
                _client.MessageCreated += _client_MessageCreated;
            }

            if(EnableGuildMemberAdded) {
                _client.GuildMemberAdded += _client_GuildMemberAdded;
            }

            if(EnableGuildBanAdded) {
                _client.GuildBanAdded += _client_GuildBanAdded;
            }
        }

        private Task _client_GuildBanAdded(DiscordClient client, GuildBanAddEventArgs e) {
            throw new NotImplementedException();
        }

        private Task _client_GuildMemberAdded(DiscordClient client, GuildMemberAddEventArgs e) {
            throw new NotImplementedException();
        }

        private Task _client_MessageCreated(DiscordClient client, MessageCreateEventArgs e) {
            throw new NotImplementedException();
        }
    }
}