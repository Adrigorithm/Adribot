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

namespace Adribot.src.commands.utilities;

public class TagCommands(TagService _tagService) : ApplicationCommandModule
{
    [SlashCommandPermissions(Permissions.SendMessages)]
    [SlashCommand("tag", "Display (information about) a tag")]
    public async Task ExecuteTagTaskAsync(InteractionContext ctx, [Option("tag", "The tag name to retrieve corresponding tag")] string tagName, [Option("mode", "Tag related operation to perform")] CrudOperation operation = CrudOperation.GET, [Option("content", "Update current tag content")] string newContent = null)
    {
        switch (operation)
        {
            case CrudOperation.GET:
            case CrudOperation.INFO:
                Tag? tag = _tagService.TryGetTag(tagName, ctx.Guild.Id);
                if (tag is null)
                {
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                        new DiscordMessageBuilder().WithContent($"Tag `{tagName}` could not be found")).AsEphemeral());
                }
                else
                {
                    DiscordMessageBuilder messageBuilder = operation == CrudOperation.GET ?
                        new DiscordMessageBuilder().WithContent($"**{tag.Name}**\n{tag.Content}") :
                        new DiscordMessageBuilder().AddEmbed(tag.GenerateEmbedBuilder());

                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(messageBuilder));
                }

                break;
            case CrudOperation.NEW:
            case CrudOperation.SET:
                (Tag?, string?) tempTag = _tagService.CreateTempTag(ctx.Guild.Id, ctx.Member.Id, tagName, newContent, ctx.Interaction.CreationTimestamp, operation == CrudOperation.SET);
                
                if (tempTag.Item1 is null)
                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                        new DiscordMessageBuilder().WithContent(tempTag.Item2)).AsEphemeral());
                else
                {
                    _tagService.SetTag(ctx.Guild.Id, ctx.Member.Id, tempTag.Item1);

                    await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                        new DiscordMessageBuilder().WithContent("Tag `" + tagName + "` craeted." + Environment.NewLine + "Check it out using `/tag {" + tagName + "} {GET|INFO}`")).AsEphemeral());
                }

                break;
            case CrudOperation.DELETE:
                if (!_tagService.TryRemoveTag(tagName, ctx.Guild.Id))
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
                IEnumerable<Tag> tags = _tagService.GetAllTags(ctx.Guild.Id);
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
}