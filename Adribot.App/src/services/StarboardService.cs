using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adribot.Data.Repositories;
using Adribot.Services.Providers;
using Discord;
using Discord.WebSocket;
using Microsoft.IdentityModel.Tokens;

namespace Adribot.Services;

public sealed class StarboardService : BaseTimerService
{
    private readonly DGuildRepository _starboardRepository;
    /// <summary>
    /// A dictionary where the Key is a guild id
    /// </summary>
    private Dictionary<ulong, (ulong channelId, List<Emote> starEmotes, List<Emoji> starEmojis, int? threshold)>? _outputChannels;

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
            (ulong channelId, List<Emote> starEmotes, List<Emoji> starEmojis, int? threshold) starboard = _outputChannels[textChannel.GuildId];
            IEnumerable<KeyValuePair<IEmote, ReactionMetadata>>? starEmojiKvp = message.Reactions.Where(r => starboard.starEmotes.Contains(r.Key) || starboard.starEmojis.Contains(r.Key));
            var starEmojiCount = starEmojiKvp.IsNullOrEmpty()
                ? 0
                : starEmojiKvp.Sum(kvp => kvp.Value.ReactionCount);

            if (starEmojiCount >= starboard.threshold)
            {
                var starReactionsString = new StringBuilder();
                starEmojiKvp.ToList().ForEach(kvp => starReactionsString.AppendLine($"{kvp.Key.Name}: x{kvp.Value.ReactionCount}!"));
                
                var embed = new EmbedBuilder
                {
                    Author = new EmbedAuthorBuilder().WithName(message.Author.Mention),
                    Color = Config.EmbedColour,
                    Description = message.Content,
                    Title = starReactionsString.ToString(),
                    Footer = new EmbedFooterBuilder().WithIconUrl(message.GetJumpUrl())
                };
                
                await textChannel.SendMessageAsync(embed: embed.Build(), allowedMentions: AllowedMentions.All);
            }
        }
    }

    public void Configure(ulong guildId, ulong channelId, List<Emote> emotes, List<Emoji> emojis, int starThreshold)
    {
        _starboardRepository.SetStarboard(guildId, channelId, emotes, emojis, starThreshold);
        
        _outputChannels ??= _starboardRepository.GetStarboards();
    }
}
