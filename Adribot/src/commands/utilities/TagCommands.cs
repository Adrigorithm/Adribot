using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.entities.utilities;
using Adribot.src.services;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Adribot.src.commands.utilities
{
    public class TagCommands : ApplicationCommandModule
    {
        public TagService TagService { get; set; }

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
                    {
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"Tag `{tagName}` could not be found")).AsEphemeral());
                    }
                    else
                    {
                        DiscordMember member = await ctx.Guild.GetMemberAsync(tag.DMemberId);
                        DiscordMessageBuilder messageBuilder = operation == CrudOperation.GET ?
                            new DiscordMessageBuilder().WithContent($"**{tag.Name}**\n{tag.Content}") :
                            new DiscordMessageBuilder().WithEmbed(tag.GenerateEmbedBuilder());

                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(messageBuilder));
                    }

                    break;
                case CrudOperation.NEW:
                case CrudOperation.SET:
                    Tag? newTag = CreateTag(ctx, tagName, newContent);
                    if (newTag is null || !await TagService.TrySetTagAsync(newTag, operation == CrudOperation.SET))
                    {
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"{(newTag is null ? "Content option cannot be empty when creating new tags." : $"A tag with the same name already exists.\nUse `SET` mode to force the change.")}")).AsEphemeral());
                    }
                    else
                    {
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent("Tag `" + tagName + "` craeted." + Environment.NewLine + "Check it out using `/tag {" + tagName + "} {GET|INFO}`")).AsEphemeral());
                    }

                    break;
                case CrudOperation.DELETE:
                    if (!TagService.TryRemoveTag(tagName, ctx.Guild.Id))
                    {
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"A tag with tagname `{tagName}` could not be found.")).AsEphemeral());
                    }
                    else
                    {
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"Tag `{tagName}` disappeared in the void.")).AsEphemeral());
                    }

                    break;
                case CrudOperation.LIST:
                    IEnumerable<Tag> tags = TagService.GetAllTags(ctx.Guild.Id);
                    if (tags.Count() == 0)
                    {
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"No tags could be found.")).AsEphemeral());
                    }
                    else
                    {
                        var tagStringBuilder = new StringBuilder();
                        for (var i = 0; i < tags.Count(); i++)
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

        private Tag? CreateTag(InteractionContext context, string tagName, string content) => string.IsNullOrWhiteSpace(content)
                ? null
                : new Tag
                {
                    Content = content,
                    Date = context.Interaction.CreationTimestamp.UtcDateTime,
                    DMemberId = context.Member.Id,
                    Name = tagName,
                    DGuildId = context.Guild.Id
                };
    }
}
