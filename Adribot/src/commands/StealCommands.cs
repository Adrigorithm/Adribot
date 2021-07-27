﻿using Adribot.src.helpers.constants;
using Adribot.src.helpers.extensions;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adribot.src.commands
{
    [Group("steal")]
    class StealCommands : BaseCommandModule
    {
        [Command("emoji")]
        [Description("Steals emoji from a message")]
        [RequirePermissions(Permissions.ManageEmojis)]
        public async Task StealEmojiAsync(CommandContext ctx, [Description("Pull emoji from message reactions instead of message content")] bool fromReactions = false, [Description("Specify if more emojis are present (0-based)")] int index = 0) {
            var emojiMessage = ctx.Message.ReferencedMessage;
            var matches = Regex.Matches(emojiMessage.Content, RegexPatterns.EmojiRegex);

            if(index > -1) {
                using(var client = new HttpClient()) {
                    try {
                        var url = $"https://cdn.discordapp.com/emojis/{matches[index].Groups[2].Value}.png";
                        if(fromReactions) {
                            url = emojiMessage.Reactions[index].Emoji.Url;
                        }
                        
                        using(var httpClient = new HttpClient()) {
                            var mStream = new MemoryStream();
                            await (await httpClient.GetStreamAsync(url)).CopyToAsync(mStream);

                            if(fromReactions) {
                                await ctx.Guild.CreateEmojiAsync(
                                    emojiMessage.Reactions[index].Emoji.Name,
                                    mStream,
                                    null,
                                    "stolen emoji lmao.");
                            } else {
                                await ctx.Guild.CreateEmojiAsync(
                                    matches[index].Groups[1].Value,
                                    mStream,
                                    null,
                                    "stolen emoji lmao.");
                            }
                        }
                    } catch(Exception e) {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        [Command("avatar")]
        [Description("Steals avatar of a given user")]
        public async Task StealAvatarAsync(CommandContext ctx, [Description("Mention the user if not replying to the user")] DiscordMember member = null) {
            var user = member ?? ctx.Message.ReferencedMessage.Author;
            if(!await ctx.Member.TrySendMessageAsync(user.AvatarUrl)) {
                await ctx.RespondAsync("Allow private messages to get the avatar image.");
            }
        }

        //[Command("sticker")]
        //[Description("Steals sticker from a message")]
        //public async Task StealStickerAsync(CommandContext ctx, [Description("Specify if more stickers are present (0-based)")] int index = 0) {
        //    var stickers = ctx.Message.ReferencedMessage.Stickers;
        //    if(stickers.Count > 0 && stickers.Count > index) {
        //        // Not supported by Lib, To be self-implement 
        //    }
        //}
    }
}
