using Adribot.services;
using DSharpPlus;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adribot.src.services
{
    public sealed class StarboardService : BaseTimerService
    {
        private readonly Dictionary<ulong, int> _starredReactions = new();

        public StarboardService(DiscordClient client, int timerInterval = 10) : base(client, timerInterval) =>
            client.MessageReactionAdded += MessageReactionAdded;

        private Task MessageReactionAdded(DiscordClient sender, DSharpPlus.EventArgs.MessageReactionAddEventArgs args) => throw new System.NotImplementedException();
    }
}
