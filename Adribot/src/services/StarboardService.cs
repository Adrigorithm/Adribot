using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.config;
using Adribot.src.data;
using Adribot.src.entities.discord;
using DSharpPlus;
using DSharpPlus.Entities;

namespace Adribot.src.services
{
    public sealed class StarboardService : BaseTimerService
    {
        /// <summary>
        /// A dictionary where the Key is a guild id
        /// </summary>
        private readonly Dictionary<ulong, (ulong channelId, DiscordEmoji? starEmoji)> _outputChannels;

        public StarboardService(DiscordClient client, int timerInterval = 10) : base(client, timerInterval)
        {
            client.MessageReactionAdded += MessageReactionAddedAsync;

            var outputChannels = new List<DGuild>();
            using (var database = new DataManager())
            {
                outputChannels = database.GetDGuildsStarboardNotNull();
            }

            if (outputChannels.Count > 0)
                _outputChannels = outputChannels.ToDictionary(dg => dg.DGuildId, dg => ((ulong)dg.StarboardChannel, DiscordEmoji.FromName(client, dg.StarEmoji ?? ":star:")));
        }

        private async Task MessageReactionAddedAsync(DiscordClient sender, DSharpPlus.EventArgs.MessageReactionAddEventArgs args)
        {
            if (_outputChannels.ContainsKey(args.Guild.Id))
            {
                var starEmojiCount = args.Message.Reactions.Count(r => r.Emoji == _outputChannels[args.Guild.Id].starEmoji);
                if (starEmojiCount >= 3)
                {
                    await args.Guild.GetChannel(_outputChannels[args.Guild.Id].channelId).SendMessageAsync(new DiscordMessageBuilder().WithEmbed(new DiscordEmbedBuilder
                    {
                        Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = $"<@{args.User.Id}" },
                        Color = new DiscordColor(Config.Configuration.EmbedColour),
                        Description = args.Message.Content,
                        Title = $"{_outputChannels[args.Guild.Id].starEmoji.Name} reacted {starEmojiCount} times!",
                        Footer = new DiscordEmbedBuilder.EmbedFooter() { Text = args.Message.JumpLink.OriginalString }
                    }));
                }
            }
        }

        public void Configure(ulong guildId, ulong channelId, string emoji)
        {
            using var database = new DataManager();

            DGuild guild = database.GetAllInstances<DGuild>().First(g => g.DGuildId == guildId);
            guild.StarboardChannel = channelId;
            guild.StarEmoji = guild.StarEmoji is null
                ? (emoji is null ? "star" : emoji)
                : (emoji is null ? guild.StarEmoji : emoji);

            _outputChannels[guildId] = (channelId, DiscordEmoji.FromName(Client, $":{guild.StarEmoji}:"));
            database.UpdateInstance(guild);
        }
    }
}
