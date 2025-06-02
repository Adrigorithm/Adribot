using System.Threading.Tasks;
using Adribot.Services.Providers;
using Discord;
using Discord.WebSocket;

namespace Adribot.Services;

public class StarboardService
{
    public StarboardService(DiscordClientProvider clientProvider)
    {
        clientProvider.Client.ReactionAdded += ClientOnReactionAdded;
        clientProvider.Client.ReactionRemoved += ClientOnReactionRemoved;
        clientProvider.Client.ReactionsCleared += ClientOnReactionsCleared;
    }

    private Task ClientOnReactionsCleared(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2)
    {
        throw new System.NotImplementedException();
    }

    private Task ClientOnReactionRemoved(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2, SocketReaction arg3)
    {
        throw new System.NotImplementedException();
    }

    private Task ClientOnReactionAdded(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2, SocketReaction arg3)
    {
        throw new System.NotImplementedException();
    }
}
