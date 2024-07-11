using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.entities.utilities;
using Adribot.src.services;
using Discord;
using Discord.Interactions;

namespace Adribot.src.commands.utilities;

public class TagCommands(TagService _tagService) : InteractionModuleBase
{
    [RequireUserPermission(ChannelPermission.SendMessages)]
    [SlashCommand("tag", "Display (information about) a tag")]
    [RequireContext(ContextType.Guild)]
    public async Task ExecuteTagTaskAsync(InteractionContext ctx, [Summary("mode", "Tag related operation to perform")] CrudOperation operation = CrudOperation.Get, [Summary("tag", "The tag name to retrieve corresponding tag")] string? tagName = null, [Summary("content", "Update current tag content")] string? newContent = null)
    {
        switch (operation)
        {
            case CrudOperation.Get:
            case CrudOperation.Info:
                if (string.IsNullOrWhiteSpace(tagName))
                    await RespondAsync("A tagName cannot be whitespace", ephemeral: true);

                Tag? tag = _tagService.TryGetTag(tagName, ctx.Guild.Id);

                if (tag is null)
                    await RespondAsync($"Tag `{tagName}` could not be found", ephemeral: true);
                else
                {
                    if (operation == CrudOperation.Get)
                        await RespondAsync($"**{tag.Name}**\n{tag.Content}");
                    else 
                        await RespondAsync(embed: tag.GenerateEmbedBuilder().Build());
                }

                break;
            case CrudOperation.New:
            case CrudOperation.Set:
                (Tag?, string?) tempTag = _tagService.CreateTempTag(ctx.Guild.Id, ctx.User.Id, tagName, newContent, ctx.Interaction.CreatedAt, operation == CrudOperation.Set);

                if (tempTag.Item1 is null)
                    await RespondAsync(tempTag.Item2, ephemeral: true);
                else
                {
                    _tagService.SetTag(ctx.Guild.Id, ctx.User.Id, tempTag.Item1);

                    await RespondAsync($"Tag `{tagName}` {(operation == CrudOperation.Set ? "updated" : "created")}.{Environment.NewLine}Check it out using `/tag {tagName} [GET|INFO]`");
                }

                break;
            case CrudOperation.Delete:
                if (!_tagService.TryRemoveTag(tagName, ctx.Guild.Id))
                    await RespondAsync($"A tag with tagname `{tagName}` could not be found.", ephemeral: true);
                else
                {
                    await ReplyAsync($"Tag `{tagName}` disappeared in the void.");
                }

                break;
            case CrudOperation.List:
                IEnumerable<Tag> tags = _tagService.GetAllTags(ctx.Guild.Id);
                if (tags.Count() == 0)
                    await RespondAsync("No tags could be found.", ephemeral: true);
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

                    await RespondAsync($"Available tags:\n{tagStringBuilder}", ephemeral: true);
                }

                break;
            default:
                throw new NotImplementedException($"Tag operation {nameof(operation)} is not supported.");
        }
    }
}
