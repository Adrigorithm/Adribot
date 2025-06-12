using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adribot.Data.Repositories;
using Adribot.Entities.Discord;
using Adribot.Entities.Utilities;
using Adribot.Services.Providers;
using Discord;
using Discord.WebSocket;

namespace Adribot.Services;

public class StarboardService
{
    private readonly StarboardRepository _starboardRepository;
    private readonly DGuildRepository _dGuildRepository;

    public StarboardService(DiscordClientProvider clientProvider, StarboardRepository starboardRepository, DGuildRepository guildRepository)
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

        if (starboard is null)
            return;

        Dictionary<string, int> emoteStrings = [];

        starboard.EmojiStrings.ForEach(es =>
        {
            var emojiString = es.ToString();

            if (emojiString == arg3.Emote.ToString())
            {
                var isPresent = emoteStrings.TryGetValue(emojiString, out var value);

                if (isPresent)
                    emoteStrings[emojiString] = value + 1;
                else
                    emoteStrings.Add(emojiString, 1);
            }
        });

        starboard.EmoteStrings.ForEach(es =>
        {
            var emoteString = es.ToString();

            if (emoteString == arg3.Emote.ToString())
            {
                var isPresent = emoteStrings.TryGetValue(emoteString, out var value);

                if (isPresent)
                    emoteStrings[emoteString] = value + 1;
                else
                    emoteStrings.Add(emoteString, 1);
            }
        });

        MessageLink? starredMessageLink = starboard.MessageLinks.FirstOrDefault(ml => ml.OriginalMessageId == arg1.Id);

        if (starredMessageLink == null)
            return;

        IGuildChannel? starboardChannel = await channel.Guild.GetChannelAsync(starboard.ChannelId);

        if (starboardChannel is not ITextChannel textChannel)
            return;

        if (emoteStrings.Count < starboard.Threshold)
        {
            await textChannel.DeleteMessageAsync(starredMessageLink.ReferenceMessageId);

            return;
        }

        IMessage? starMessage = await textChannel.GetMessageAsync(starredMessageLink.ReferenceMessageId);

        if (starMessage is not SocketUserMessage userMessage)
            return;

        await userMessage.ModifyAsync(m => m.Embed = StarredMessageEmbed(emoteStrings).Build());
    }

    private async Task ClientOnReactionAddedAsync(Cacheable<IUserMessage, ulong> arg1, Cacheable<IMessageChannel, ulong> arg2, SocketReaction arg3)
    {
        if (arg2.Value is not ITextChannel channel)
            return;

        Starboard? starboard = _starboardRepository.GetStarboardConfiguration(channel.Guild.Id);

        if (starboard is null)
            return;

        Dictionary<string, int> emoteStrings = [];

        starboard.EmojiStrings.ForEach(es =>
        {
            var emojiString = es.ToString();

            if (emojiString == arg3.Emote.ToString())
            {
                var isPresent = emoteStrings.TryGetValue(emojiString, out var value);

                if (isPresent)
                    emoteStrings[emojiString] = value + 1;
                else
                    emoteStrings.Add(emojiString, 1);
            }
        });

        starboard.EmoteStrings.ForEach(es =>
        {
            var emoteString = es.ToString();

            if (emoteString == arg3.Emote.ToString())
            {
                var isPresent = emoteStrings.TryGetValue(emoteString, out var value);

                if (isPresent)
                    emoteStrings[emoteString] = value + 1;
                else
                    emoteStrings.Add(emoteString, 1);
            }
        });

        if (emoteStrings.Count < starboard.Threshold)
            return;

        MessageLink? starredMessageLink = starboard.MessageLinks.FirstOrDefault(ml => ml.OriginalMessageId == arg1.Id);
        IGuildChannel? starboardChannel = await channel.Guild.GetChannelAsync(starboard.ChannelId);

        if (starboardChannel is not ITextChannel textChannel)
            return;

        if (starredMessageLink is null)
        {
            IUserMessage message = await textChannel.SendMessageAsync(embed: StarredMessageEmbed(emoteStrings).Build());

            _starboardRepository.AddMessageLink(new MessageLink
            {
                OriginalMessageId = arg1.Id,
                Starboard = starboard,
                ReferenceMessageId = message.Id
            });

            return;
        }

        IMessage? starMessage = await textChannel.GetMessageAsync(starredMessageLink.ReferenceMessageId);

        if (starMessage is not SocketUserMessage userMessage)
            return;

        await userMessage.ModifyAsync(m => m.Embed = StarredMessageEmbed(emoteStrings).Build());
    }

    private EmbedBuilder StarredMessageEmbed(Dictionary<string, int> emoteStrings)
    {
        var emoteValues = new StringBuilder();

        foreach (var key in emoteStrings.Keys)
            emoteValues.AppendLine($"{key} x {emoteStrings[key]}");

        return new EmbedBuilder()
        {
            Author = new EmbedAuthorBuilder { Name = "Adrialgorithm" },
            Fields =
            [
                new EmbedFieldBuilder { Name = "Reactions:", IsInline = false, Value = emoteValues.ToString() }
            ],
            Title = ""
        };
    }

    public void Configure(ulong guildId, ulong channelId, List<string> emotesList, List<string> emojisList, int amount)
    {
        Starboard? starboard = _starboardRepository.GetStarboardConfiguration(guildId);

        if (starboard is null)
        {
            DGuild guild = _dGuildRepository.GetGuild(guildId);

            _starboardRepository.SetStarboardConfiguration(guildId, new Starboard
            {
                ChannelId = channelId,
                Threshold = amount,
                EmojiStrings = emojisList,
                EmoteStrings = emotesList,
                DGuild = guild
            }, false);
        }
        else
        //TODO: Add a way to remove/update messages made using previous starboard configuration
        {
            starboard.Threshold = amount;
            starboard.EmojiStrings = emojisList;
            starboard.EmoteStrings = emotesList;
            starboard.ChannelId = channelId;

            _starboardRepository.SetStarboardConfiguration(guildId, starboard, true);
        }
    }
}
