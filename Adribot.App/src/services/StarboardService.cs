using System;
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
using Microsoft.IdentityModel.Tokens;

namespace Adribot.Services;

public sealed class StarboardService : BaseTimerService
{
    private readonly StarboardRepository _starboardRepository;
    private readonly DGuildRepository _guildRepository;

    private IEnumerable<DMessage> _starredMessages;
    private IEnumerable<Starboard> _starboards;

    public StarboardService(StarboardRepository starboardRepository, DGuildRepository guildRepository, DiscordClientProvider clientProvider, SecretsProvider secretsProvider, int timerInterval = 10) : base(clientProvider, secretsProvider, timerInterval)
    {
        clientProvider.Client.ReactionAdded += ReactionAddedAsync;

        _starboardRepository = starboardRepository;
    }

    private async Task ReactionAddedAsync(Cacheable<IUserMessage, ulong> cacheable1, Cacheable<IMessageChannel, ulong> cacheable2, SocketReaction reaction)
    {
        _starboards ??= _starboardRepository.GetStarboards();
        _starredMessages ??= _starboardRepository.GetStarredMessages();
        
        IMessageChannel channel = await cacheable2.GetOrDownloadAsync();
        IUserMessage message = await cacheable1.GetOrDownloadAsync();

        if (channel is not ITextChannel textChannel)
            return;

        var starboard = _starboards.FirstOrDefault(s => s.DGuild.GuildId == textChannel.GuildId);

        // Starboard configuration not setup
        if (starboard is null)
            return;


        IEnumerable<KeyValuePair<IEmote, ReactionMetadata>>? starEmojiKvp = message.Reactions.Where(r => starboard.StarEmotes.Contains(r.ToString()) || starboard.StarEmojis.Contains(r.ToString()));
        var starEmojiCount = starEmojiKvp.IsNullOrEmpty()
        ? 0
        : starEmojiKvp.Sum(kvp => kvp.Value.ReactionCount);

        if (starEmojiCount >= starboard.Threshold)
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

    public void Configure(ulong guildId, ulong channelId, List<string> emotes, List<string> emojis, int starThreshold)
    {
        _starboards ??= _starboardRepository.GetStarboards();
        var starboard = _starboards.FirstOrDefault(s => s.DGuild.GuildId == guildId);

        if (starboard is null) {
            var guild = _guildRepository.GetGuild(guildId);
            starboard = new Starboard {
                Channel = channelId,
                DGuild = guild,
                StarEmojis = emojis,
                StarEmotes = emotes,
                Threshold = starThreshold
            };
        }
        else
        {
            starboard.Channel = channelId;
            starboard.StarEmotes = emotes;
            starboard.StarEmojis = emojis;
            starboard.Threshold = starThreshold;
        }

        _starboardRepository.SetupStarBoard(starboard);
    }
}
