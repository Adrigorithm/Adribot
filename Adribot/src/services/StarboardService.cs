using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.data.repositories;
using Adribot.src.services.providers;
using DSharpPlus;
using DSharpPlus.Entities;

namespace Adribot.src.services;

public sealed class StarboardService : BaseTimerService
{
    private readonly DGuildRepository _starboardRepository;
    /// <summary>
    /// A dictionary where the Key is a guild id
    /// </summary>
    private readonly Dictionary<ulong, (ulong channelId, string? starEmoji, int? threshold)> _outputChannels = [];

    public StarboardService(DGuildRepository starboardRepository, DiscordClientProvider clientProvider, SecretsProvider secretsProvider, int timerInterval = 10) : base(clientProvider, secretsProvider, timerInterval)
    {
        clientProvider.Client.MessageReactionAdded += MessageReactionAddedAsync;

        _starboardRepository = starboardRepository;
        _outputChannels = _starboardRepository.GetStarboards();
    }

    private async Task MessageReactionAddedAsync(DiscordClient sender, DSharpPlus.EventArgs.MessageReactionAddEventArgs args)
    {

        if (_outputChannels.ContainsKey(args.Guild.Id))
        {
#pragma warning disable IDE0007 // Use implicit type
            (var channelId, string? starEmoji, var threshold) = _outputChannels[args.Guild.Id];
#pragma warning restore IDE0007 // Use implicit type

            var starEmojiCount = args.Message.Reactions.Count(r => r.Emoji == starEmoji);
            
            if (starEmojiCount >= threshold)
            {
                await args.Guild.GetChannel(channelId).SendMessageAsync(new DiscordMessageBuilder().AddEmbed(new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = $"{args.User.Mention}" },
                    Color = new DiscordColor(Config.EmbedColour),
                    Description = args.Message.Content,
                    Title = $":{starEmoji ?? "star"}: reacted {starEmojiCount} times!",
                    Footer = new DiscordEmbedBuilder.EmbedFooter() { Text = args.Message.JumpLink.OriginalString }
                }));
            }
        }
    }

    public void Configure(ulong guildId, ulong channelId, string? emoji, int starThreshold)
    {
        _starboardRepository.SetStarboard(guildId, channelId, emoji, starThreshold);

        _outputChannels[guildId] = (channelId, emoji, starThreshold);
    }
}
