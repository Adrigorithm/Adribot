using Adribot.config;
using Adribot.constants.enums;
using Adribot.data;
using Adribot.entities.utilities;
using Adribot.extensions;
using Adribot.src.constants.enums;
using Adribot.src.helpers;
using Adribot.src.services;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.commands.utilities
{
    public class UtilityCommands : ApplicationCommandModule
    {
        public TagService TagService { get; set; }
        public RemindMeSerivce RemindMeService { get; set; }
        public StarboardService StarboardService { get; set; }

        [SlashCommandPermissions(Permissions.SendMessages)]
        [SlashCommand("tag", "Display (information about) a tag")]
        public async Task ExecuteTagTaskAsync(InteractionContext ctx, [Option("tag", "The tag name to retrieve corresponding tag")] string tagName, [Option("mode", "Tag related operation to perform")] CrudOperation operation = CrudOperation.GET, [Option("content", "Update current tag content")] string newContent = null)
        {
            switch (operation)
            {
                case CrudOperation.GET:
                case CrudOperation.INFO:
                    Tag? tag = TagService.TryGetTag(tagName, ctx.Guild.Id);
                    if (tag is null)
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"Tag `{tagName}` could not be found")).AsEphemeral());
                    else
                    {
                        DiscordMember member = await ctx.Guild.GetMemberAsync(tag.DMemberId);

                        DiscordMessageBuilder messageBuilder = operation == CrudOperation.GET ?
                            new DiscordMessageBuilder().WithContent($"{tag.Content}") :
                            new DiscordMessageBuilder().WithEmbed(new DiscordEmbedBuilder()
                            {
                                Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = Convert.ToString(member is null ? "404" : $"<@{member.Id}>") },
                                Color = new DiscordColor(Config.Configuration.EmbedColour),
                                ImageUrl = member?.AvatarUrl,
                                Description = tag.Content,
                                Timestamp = tag.Date,
                                Title = tag.Name
                            });

                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(messageBuilder));
                    }

                    break;
                case CrudOperation.NEW:
                case CrudOperation.SET:
                    Tag? newTag = CreateTag(ctx, tagName, newContent);
                    if (newTag is null || !await TagService.TrySetTagAsync(newTag, operation == CrudOperation.SET))
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"{(newTag is null ? "Content option cannot be empty when creating new tags." : $"A tag with the same name already exists.\nUse `SET` mode to force the change.")}")).AsEphemeral());
                    else
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent("Tag `" + tagName + "` craeted." + Environment.NewLine + "Check it out using `/tag {" + tagName + "} {GET|INFO}`")).AsEphemeral());

                    break;
                case CrudOperation.DELETE:
                    if (!TagService.TryRemoveTag(tagName, ctx.Guild.Id))
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"A tag with tagname `{tagName}` could not be found.")).AsEphemeral());
                    else
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"Tag `{tagName}` disappeared in the void.")).AsEphemeral());

                    break;
                case CrudOperation.LIST:
                    IEnumerable<Tag> tags = TagService.GetAllTags(ctx.Guild.Id);
                    if (tags.Count() == 0)
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"No tags could be found.")).AsEphemeral());
                    else
                    {
                        var tagStringBuilder = new StringBuilder();
                        for (int i = 0; i < tags.Count(); i++)
                        {
                            if (i == tags.Count() - 1)
                                tagStringBuilder.Append($"`{tags.ElementAt(i).Name}`");
                            else
                                tagStringBuilder.Append($"`{tags.ElementAt(i).Name}`, ");
                        }

                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"Available tags:\n{tagStringBuilder}")).AsEphemeral());
                    }

                    break;
                default:
                    throw new NotImplementedException($"Tag operation {nameof(operation)} is not supported.");
            }
        }

        private Tag? CreateTag(InteractionContext context, string tagName, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                return null;

            return new Tag
            {
                Content = content,
                Date = context.Interaction.CreationTimestamp.UtcDateTime,
                DMemberId = context.Member.Id,
                Name = tagName,
                DGuildId = context.Guild.Id
            };
        }

        [SlashCommand("remindme", "Set an alert for something ahead of time")]
        public async Task ExecuteRemindTaskAsync(InteractionContext ctx, [Option("task", "What you should be reminded of")] string taskTodo, [Option("unit", "Time unit to be muliplied by the next factor parameter")] TimeSpanType timeUnit, [Option("factor", "Amount of instances of the specified time unit")] long factor, [Option("channel", "Only set this if you have dms blocked (or muted the bot)")] DiscordChannel altChannel = null)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            DateTimeOffset endDate = timeUnit.ToEndDate((int)factor, now);

            if (endDate - now < TimeSpan.FromMinutes(1))
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                    new DiscordMessageBuilder().WithContent($"Remind me's should be set at least 1 minute ahead in time.")).AsEphemeral());
            else
            {
                await RemindMeService.AddRemindMeAsync(new Reminder
                {
                    Channel = altChannel?.Id,
                    Content = taskTodo,
                    Date = now,
                    DGuildId = ctx.Guild.Id,
                    DMemberId = ctx.Member.Id,
                    EndDate = endDate
                });

                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                    new DiscordMessageBuilder().WithContent($"I will remind you at {endDate:g}")).AsEphemeral());
            }
        }

        [SlashCommand("config", "Configure several bot services")]
        [RequirePermissionOrDev(Permissions.Administrator)]
        public async Task ConfigureBotAsync(InteractionContext ctx, [Option("configOption", "option to configure")] ConfiguratorOption configOption, [Option("channel", "defaults to current channel")] DiscordChannel channel = null, [Option("emoji", "emoji name without `:` - defaults to star emoji")] string emoji = null)
        {
            using var database = new DataManager(ctx.Client);

            switch (configOption)
            {
                case ConfiguratorOption.STARBOARD:
                    StarboardService.Configure(ctx.Guild.Id, channel?.Id ?? ctx.Channel.Id, emoji);

                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                        new DiscordMessageBuilder().WithContent($"Starred messages will now appear in {channel.Mention}")).AsEphemeral());

                    break;
                default:
                    break;
            }
        }
    }
}
