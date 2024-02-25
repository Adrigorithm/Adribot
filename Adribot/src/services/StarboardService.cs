using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.data;
using Adribot.src.entities.discord;
using Adribot.src.services.providers;
using DSharpPlus;
using DSharpPlus.Entities;

namespace Adribot.src.services;

public sealed class StarboardService : BaseTimerService
{
    /// <summary>
    /// A dictionary where the Key is a guild id
    /// </summary>
    private readonly Dictionary<ulong, (ulong channelId, DiscordEmoji starEmoji, int threshold)> _outputChannels;

    public StarboardService(DiscordClientProvider clientProvider, SecretsProvider secretsProvider, int timerInterval = 10) : base(clientProvider, secretsProvider, timerInterval)
    {
        clientProvider.Client.MessageReactionAdded += MessageReactionAddedAsync;

        var outputChannels = new List<DGuild>();
        using (var database = new DataManager())
        {
            outputChannels = database.GetDGuildsStarboardNotNull();
        }

        if (outputChannels.Count > 0)
            _outputChannels = outputChannels.ToDictionary(dg => dg.GuildId, dg => ((ulong)dg.StarboardChannel, DiscordEmoji.FromName(Client, dg.StarEmoji), (int)dg.StarThreshold));
    }

    private async Task MessageReactionAddedAsync(DiscordClient sender, DSharpPlus.EventArgs.MessageReactionAddEventArgs args)
    {

        if (_outputChannels.ContainsKey(args.Guild.Id))
        {
            (var channelId, DiscordEmoji starEmoji, var threshold) = _outputChannels[args.Guild.Id];

            var starEmojiCount = args.Message.Reactions.Count(r => r.Emoji == starEmoji);
            if (starEmojiCount >= threshold)
            {
                await args.Guild.GetChannel(channelId).SendMessageAsync(new DiscordMessageBuilder().AddEmbed(new DiscordEmbedBuilder
                {
                    Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = $"{args.User.Mention}" },
                    Color = new DiscordColor(Config.EmbedColour),
                    Description = args.Message.Content,
                    Title = $"{starEmoji.Name} reacted {starEmojiCount} times!",
                    Footer = new DiscordEmbedBuilder.EmbedFooter() { Text = args.Message.JumpLink.OriginalString }
                }));
            }
        }
    }

    public void Configure(ulong guildId, ulong channelId, string? emoji, int starThreshold)
    {
        using var database = new DataManager();

        DGuild guild = database.GetAllInstances<DGuild>().First(g => g.GuildId == guildId);
        guild.StarboardChannel = channelId;
        guild.StarEmoji = guild.StarEmoji is null
            ? (emoji is null ? "star" : emoji)
            : (emoji is null ? guild.StarEmoji : emoji);
        guild.StarThreshold = starThreshold;

        _outputChannels[guildId] = (channelId, DiscordEmoji.FromName(Client, $":{guild.StarEmoji}:"), starThreshold);
        database.UpdateInstance(guild);
    }
}
