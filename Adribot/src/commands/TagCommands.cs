using Adribot.src.entities;
using Adribot.src.entities.source;
using Adribot.src.helpers.extensions;
using Adribot.src.services.spec;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.commands
{
    [Group("tag")]
    class TagCommands : BaseCommandModule
    {
        private List<Tag> _tags;

        public TagCommands() {
            GetTagsAsync();
        }

        private void GetTagsAsync() {
            using(var db = new DBController()) {
                _tags = db.Tags.ToList();
            }
        }

        [Command()]
        [Description("Gets a tag")]
        [Aliases("get")]
        [RequirePermissions(Permissions.SendMessages)]
        public async Task GetTagAsync(CommandContext ctx, string tagName) {
            try {
                var tag = _tags.First(x => x.GuildId == ctx.Guild.Id && x.TagName == tagName.ToLower());
                DiscordMember tagAuthor;

                tagAuthor = await ctx.Guild.GetMemberAsync(tag.AuthorId);

                var tagMessage = new DiscordEmbedBuilder {
                    Author = new DiscordEmbedBuilder.EmbedAuthor {
                        Name = tagAuthor.Username,
                        IconUrl = tagAuthor.AvatarUrl
                    },
                    Title = tag.TagName,
                    Description = tag.Content
                };

                await ctx.RespondAsync(tagMessage);
            } catch(Exception) {
                await ctx.RespondAsync($"A Tag {tagName} does not exist (Or the database went offline).");
            }
        }

        [Command("create")]
        [Description("Creates a new tag")]
        [Aliases("new", "add")]
        [RequirePermissions(Permissions.ManageChannels)]
        public async Task CreateTagAsync(CommandContext ctx, string tagName, string content) {
            if(content.Length <= 4000 && tagName.Length <= 50) {
                var tag = new Tag {
                    AuthorId = ctx.Member.Id,
                    GuildId = ctx.Guild.Id,
                    Content = content,
                    TagName = tagName
                };

                using(var db = new DBController()) {
                    db.Add(tag);
                    try {
                        await db.SaveChangesAsync();
                        _tags.Add(tag);
                    } catch(Exception) {
                        await ctx.RespondAsync($"A Tag {tagName} already exists (Or the database went offline).");
                    }
                }
            }
        }
    }
}
