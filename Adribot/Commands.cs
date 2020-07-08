using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Adribot
{
    public class Commands
    {
        [Command("clear"), Description("Clears chat history"), RequirePermissions(Permissions.ManageMessages)]
        public async Task ClearChat(CommandContext ctx, [Description("Amount of messages to be deleted.")] int amount = -1) {
            var channel = ctx.Channel;

            if(amount >= 0) {
                bool old = false;
                List<DiscordMessage> messages = new List<DiscordMessage>();
                ulong lastId = ctx.Message.Id;

                for(int i = amount; i > 0; i -= 100) {
                    if(i >= 100) {
                        messages.AddRange(await channel.GetMessagesAsync(100, lastId));
                    } else {
                        messages.AddRange(await channel.GetMessagesAsync(i, lastId));
                    }

                    if(DateTime.UtcNow > messages[messages.Count - 1].CreationTimestamp.UtcDateTime.AddDays(14)) {
                        old = true;
                    }

                    await channel.DeleteMessagesAsync(messages);
                }

                // Remove command message
                await channel.DeleteMessageAsync(ctx.Message, "Remove command");

                if(old) {
                    // (Partial) failure message
                    var emojiWarning = DiscordEmoji.FromName(ctx.Client, ":warning:");
                    await channel.SendMessageAsync($"{emojiWarning} Some messages have not been removed due to them being too old.\nConsider using `$clear` instead.");
                } else {
                    // Success notification
                    var emojiSuccess = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");
                    await channel.SendMessageAsync($"{emojiSuccess} Removed `{amount}` messages successfully!");
                }
            } else {
                InteractivityModule interactivity = ctx.Client.GetInteractivityModule();

                await ctx.RespondAsync("Reply `y` to remove all messages\nThis includes pinned messages!");

                var approval = await interactivity.WaitForMessageAsync(m => m.Content.ToLower().Equals("y"), TimeSpan.FromSeconds(5));

                if(approval.Message.Author.Equals(ctx.User)) {
                    // Translate 0 to null
                    int? tBitrate = channel.Bitrate;
                    int? tUserLimit = channel.UserLimit;

                    if(channel.Bitrate.Equals(0) && channel.UserLimit.Equals(0)) {
                        tBitrate = null;
                        tUserLimit = null;
                    }

                    // Reset channel
                    var newChannel = await ctx.Guild.CreateChannelAsync(channel.Name, channel.Type, channel.Parent, tBitrate, tUserLimit, channel.PermissionOverwrites, "Force clear channel");
                    await channel.DeleteAsync("Force clear channel");

                    // Success notification
                    var emojiSuccess = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");
                    await newChannel.SendMessageAsync($"{emojiSuccess} Removed all messages successfully!");
                }
            }
        }
    }
}
