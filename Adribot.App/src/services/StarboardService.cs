using System.Collections.Generic;
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
        clientProvider.Client.ReactionAdded += ClientOnReactionAddedAsync;
        clientProvider.Client.ReactionRemoved += ClientOnReactionRemovedAsync;
        clientProvider.Client.ReactionsCleared += ClientOnReactionsClearedAsync;
        
        _starboardRepository = starboardRepository;
    }

    private async Task ClientOnReactionsClearedAsync(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2)
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

    private async Task ClientOnReactionRemovedAsync(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2, SocketReaction arg3)
    {
        if (arg2.Value is not ITextChannel channel)
            return;

        Starboard? starboard = _starboardRepository.GetStarboardConfiguration(channel.Guild.Id);
    }

    private async Task ClientOnReactionAddedAsync(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2, SocketReaction arg3)
    {
        if (arg2.Value is not ITextChannel channel)
            return;

        Starboard? starboard = _starboardRepository.GetStarboardConfiguration(channel.Guild.Id);

        if (starboard is null)
            return;

        Dictionary<string, int> emoteStrings = [];

        starboard.EmojiStrings.ForEach(es => {
            var emojiString = es.ToString();

            if (emojiString == arg3.Emote.ToString())
            {
                var isPresent = emoteStrings.TryGetValue(emojiString, out int value);

                if (isPresent)
                    emoteStrings[emojiString] = value++;
                else
                    emoteStrings.Add(emojiString, 1);
            }
        })

        starboard.EmoteStrings.ForEach(es => {
            var emoteString = es.ToString();

            if (emoteString == arg3.Emote.ToString())
            {
                var isPresent = emoteStrings.TryGetValue(emoteString, out int value);

                if (isPresent)
                    emoteStrings[emoteString] = value++;
                else
                    emoteStrings.Add(emoteString, 1);
            }
        })

        if (emoteStrings.Count < starboard.Threshold)
            return;

        MessageLink? starredMessageLink = starboard.MessageLinks.FirstOrDefault(ml => ml.OriginalMessageId == arg1.Id);

        if (starredMessageLink is null)
        {
            IGuildChannel? starboardChannel = await channel.Guild.GetChannelAsync(starboard.ChannelId);

            if (starboardChannel is not ITextChannel textChannel)
                return;

            var message = textChannel.SendMessageAsync();
            // TODO: construct enum in domain
        }
        // TODO: update existing enum
    }
}
