using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.Data.Repositories;
using Adribot.Services.Providers;
using Discord;
using Discord.WebSocket;

namespace Adribot.Services;

public sealed class StarboardService : BaseTimerService
{
    private readonly DGuildRepository _starboardRepository;
    /// <summary>
    /// A dictionary where the Key is a guild id
    /// </summary>
    private Dictionary<ulong, (ulong channelId, List<IEmote> starEmoji, int? threshold)>? _outputChannels;

    public StarboardService(DGuildRepository starboardRepository, DiscordClientProvider clientProvider, SecretsProvider secretsProvider, int timerInterval = 10) : base(clientProvider, secretsProvider, timerInterval)
    {
        clientProvider.Client.ReactionAdded += ReactionAddedAsync;

        _starboardRepository = starboardRepository;
    }

    private async Task ReactionAddedAsync(Cacheable<IUserMessage, ulong> cacheable1, Cacheable<IMessageChannel, ulong> cacheable2, SocketReaction reaction)
    {
        _outputChannels ??= _starboardRepository.GetStarboards();
        
        IMessageChannel channel = await cacheable2.GetOrDownloadAsync();
        IUserMessage message = await cacheable1.GetOrDownloadAsync();

        if (channel is ITextChannel textChannel && _outputChannels.ContainsKey(textChannel.GuildId))
        {
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            (var channelId, string? starEmoji, var threshold) = _outputChannels[textChannel.GuildId];

            KeyValuePair<IEmote, ReactionMetadata> starEmojiKvp = message.Reactions.FirstOrDefault(r => r.Key.Name == starEmoji);
            var starEmojiCount = starEmojiKvp.Key is null
                ? 0
                : starEmojiKvp.Value.ReactionCount;

            if (starEmojiCount >= threshold)
            {
                var embed = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder().WithName(message.Author.Mention),
                    Color = Config.EmbedColour,
                    Description = message.Content,
                    Title = $":{starEmoji ?? "star"}: reacted {starEmojiCount} times!",
                    Footer = new EmbedFooterBuilder().WithIconUrl(message.GetJumpUrl())
                };
                
                await textChannel.SendMessageAsync(embed: embed.Build(), allowedMentions: AllowedMentions.All);
            }
        }
    }

    public void Configure(ulong guildId, ulong channelId, IEnumerable<IEmote> emotes, int starThreshold)
    {
        _starboardRepository.SetStarboard(guildId, channelId, emotes, starThreshold);
        
        // Change this
        _outputChannels ??= _starboardRepository.GetStarboards();
        _outputChannels[guildId] = (channelId, emotes, starThreshold);
    }
}
