using System.Linq;
using System.Threading.Tasks;
using Adribot.Data.Repositories;
using Adribot.Entities.Utilities;
using Adribot.Services.Providers;
using Discord;
using Discord.WebSocket;

namespace Adribot.Services;

public class StarboardService
{
    private readonly StarboardRepository _starboardRepository;
    
    public StarboardService(DiscordClientProvider clientProvider, StarboardRepository starboardRepository)
    {
        clientProvider.Client.ReactionAdded += ClientOnReactionAdded;
        clientProvider.Client.ReactionRemoved += ClientOnReactionRemoved;
        clientProvider.Client.ReactionsCleared += ClientOnReactionsCleared;
        
        _starboardRepository = starboardRepository;
    }

    private async Task ClientOnReactionsCleared(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2)
    {
        if (arg2.Value is not ITextChannel channel)
            return;
        
        Starboard? starboard = _starboardRepository.GetStarboardConfiguration(channel.Guild.Id);
        MessageLink? starredMessageLink = starboard?.MessageLinks.FirstOrDefault(ml => ml.OriginalMessageId == arg1.Id);

        if (starredMessageLink == null)
            return;

        IGuildChannel? starboardChannel = await channel.Guild.GetChannelAsync(starboard.ChannelId);
        
        if (starboardChannel is ITextChannel textChannel)
            await textChannel.DeleteMessageAsync(starredMessageLink.ReferenceMessageId);
        
        _starboardRepository.RemoveMessageLink(starredMessageLink);
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
