using DSharpPlus;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using DSharpPlus.Entities;
using Adribot.src.services;
using Adribot.entities.utilities;
using Adribot.config;
using Adribot.extensions;
using System.Linq;
using System.Text;

namespace Adribot.commands.utilities
{
    public class UtilityCommands : ApplicationCommandModule
    {
        public TagService TagService { get; set; }

        [SlashCommand("tag", "Display (information about) a tag")]
        public async Task GetAnimalAsync(InteractionContext ctx, [Option("tag", "The tag name to retrieve corresponding tag")] string tagName, [Option("mode", "Tag related operation to perform")] CrudOperation operation = CrudOperation.GET, [Option("content", "Update current tag content")] string newContent = null)
        {
            switch (operation)
            {
                case CrudOperation.GET:
                case CrudOperation.INFO:
                    Tag? tag = TagService.TryGetTag(tagName, ctx.Guild.Id);
                    if (tag is null)
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"Tag `{tag.Name}` could not be found")).AsEphemeral());
                    else
                    {
                        DiscordMember member = await ctx.Guild.GetMemberAsync(tag.DMemberId);

                        DiscordMessageBuilder messageBuilder = operation == CrudOperation.GET ?
                            new DiscordMessageBuilder().WithContent($"{tag.Content}") :
                            new DiscordMessageBuilder().WithEmbed(new DiscordEmbedBuilder()
                            {
                                Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = Convert.ToString(member?.Mention ?? "404") },
                                Color = new DiscordColor(Config.Configuration.EmbedColour),
                                ImageUrl = member?.AvatarUrl,
                                Description = tag.Content,
                                Timestamp = tag.Date,
                                Title = tag.Name
                            });

                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(messageBuilder));
                    }

                    break;
                case CrudOperation.SET:
                case CrudOperation.UPDATE:
                    Tag? newTag = CreateTag(ctx, tagName, newContent);
                    if (newTag is null || !await TagService.TrySetTagAsync(newTag, operation == CrudOperation.UPDATE))
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"{(newTag is null ? "Content option cannot be empty when creating new tags." : "A tag with the same name already exists.\nUse Update mode to force the change.")}")).AsEphemeral());
                    else
                        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

                    break;
                case CrudOperation.REMOVE:
                    if (!TagService.TryRemoveTag(tagName, ctx.Guild.Id))
                        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                            new DiscordMessageBuilder().WithContent($"A tag with tagname {tagName} could not be found.")).AsEphemeral());
                    else
                        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

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
                Date = context.Interaction.CreationTimestamp,
                DMemberId = context.Member.Id,
                Name = tagName,
                DGuildId = context.Guild.Id
            };
        }
    }
}
