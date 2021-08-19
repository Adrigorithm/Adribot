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
        private static string[] tagNameBlacklist = {"create", "new", "add", "remove", "delete", "edit"};

        public TagCommands() {
            GetTagsAsync();
        }

        private void GetTagsAsync() {
            using(var db = new DBController()) {
                try {
                    _tags = db.Tags.ToList();
                } catch(Exception e) {
                    Console.WriteLine(e);
                    _tags = new List<Tag>();
                }
            }
        }

        [GroupCommand]
        public async Task GetTagAsync(CommandContext ctx, string tagName) {
            try {
                var tag = _tags.First(x => x.GuildId == ctx.Guild.Id && x.TagName == tagName);
                DiscordMember tagAuthor;

                tagAuthor = await ctx.Guild.GetMemberAsync(tag.Member.UserId);

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
                await ctx.RespondAsync($"A Tag `{tagName}` does not exist (Or the database went offline).");
            }
        }

        [Command("create")]
        [Description("Creates a new tag")]
        [Aliases("new", "add")]
        [RequirePermissions(Permissions.ManageChannels)]
        public async Task CreateTagAsync(CommandContext ctx, string tagName,[RemainingText] string content) {
            if(!tagNameBlacklist.Contains(tagName) && content.Length <= 4000 && tagName.Length <= 40) {
                using(var db = new DBController()) {
                    var tag = new Tag {
                        UserId = db.Members.First(x => x.UserId == ctx.Member.Id).UserId,
                        Content = content,
                        TagName = tagName,
                        GuildId = ctx.Guild.Id
                    };

                    db.Add(tag);
                    try {
                        await db.SaveChangesAsync();
                        _tags.Add(tag);
                    } catch(Exception) {
                        await ctx.RespondAsync($"A Tag `{tagName}` already exists (Or the database went offline).");
                    }
                }
            } else {
                await ctx.RespondAsync($"A tagName has a limit of `40` chars and cannot be a tag sub-command, while for the tagContent it is `4000`");
            }
        }

        [Command("remove")]
        [Description("Removes a tag")]
        [Aliases("delete")]
        [RequirePermissions(Permissions.ManageChannels)]
        public async Task DeleteTagAsync(CommandContext ctx, string tagName) {
            var tag = _tags.First(x => x.GuildId == ctx.Guild.Id && x.TagName == tagName);
            using(var db = new DBController()) {
                db.Remove(tag);
                try {
                    await db.SaveChangesAsync();
                    _tags.Remove(tag);
                } catch(Exception) {
                    await ctx.RespondAsync($"A Tag `{tagName}` doesn't exists (Or the database went offline).");
                }
            }
        }

        [Command("edit")]
        [Description("Edits a tag")]
        [RequirePermissions(Permissions.ManageChannels)]
        public async Task EditTagAsync(CommandContext ctx, string tagName, [Description("New tag content")][RemainingText] string content) {
            for(int i = 0; i < _tags.Count(); i++) {
                if(_tags[i].GuildId == ctx.Guild.Id && _tags[i].TagName == tagName) {
                    var contentOld = _tags[i].Content;
                    _tags[i].Content = content;

                    using(var db = new DBController()) {
                        db.Update(_tags[i]);
                        try {
                            await db.SaveChangesAsync();
                        } catch(Exception) {
                            _tags[i].Content = contentOld;
                            await ctx.RespondAsync($"A Tag `{tagName}` doesn't exists (Or the database went offline).");
                        }
                    }
                }
            }
        }
    }
}
