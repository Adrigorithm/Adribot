using Adribot.src.helpers.constants;
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
    class UtilityCommands : BaseCommandModule
    {
        [Command("emoji")]
        [Description("Steals emoji from a message")]
        [RequirePermissions(Permissions.ManageEmojis)]
        public async Task StealEmoji(CommandContext ctx, [Description("Specify if more emojis are present (0-based)")] int index = -1) {
            var emojiMessage = ctx.Message.ReferencedMessage.Content;
            var matches = Regex.Matches(emojiMessage, RegexPatterns.EmojiRegex);

            using(var client = new HttpClient()) {
                try {
                    if(index > -1) {
                        var url = $"https://cdn.discordapp.com/emojis/{matches[index].Groups[2].Value}.png";

                        using (var httpClient = new HttpClient()) {
                            var mStream = new MemoryStream();
                            await (await httpClient.GetStreamAsync(url)).CopyToAsync(mStream);

                            await ctx.Guild.CreateEmojiAsync(matches[index].Groups[1].Value,
                                mStream, 
                                null, 
                                "stolen emoji lmao.");
                        }
                        
                    } else {
                        // This doesn't work yet.
                        throw new NotImplementedException();

                        for(int i = 0; i < matches.Count; i++) {
                            var response = await client.GetStreamAsync($"https://cdn.discordapp.com/emojis/{matches[i].Groups[2].Value}.png");
                            await ctx.RespondAsync(new DiscordEmbedBuilder {
                                Title = "Emoji",
                                ImageUrl = "https://cdn.discordapp.com/emojis/{matches[index].Groups[1].Value}.png"
                            });
                        }
                    }
                } catch(Exception e) {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
