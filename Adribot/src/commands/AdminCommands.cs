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
    class AdminCommands : BaseCommandModule {
        public BanService BanController { private get; set; }
        public MuteService MuteController { private get; set; }

        [Command("ban")]
        [Description("Permabans a member")]
        [RequirePermissions(Permissions.BanMembers)]
        public async Task BanMemberAsync(CommandContext ctx, [Description("Member to ban")] DiscordMember member, [Description("Y u do dis?!")] string reason = "") {
            await member.BanAsync(0, reason);
        }

        [Command("ban")]
        [Description("Temporary ban member")]
        [Aliases("tban")]
        [RequirePermissions(Permissions.BanMembers)]
        public async Task BanMemberAsync(CommandContext ctx, [Description("Member to ban")] DiscordMember member, [Description("Duration: \\d[mhdwMy]")] string duration = "1w", [Description("Y u do dis?!")] string reason = "") {
            await BanController.BanAsync(
                member,
                duration.ToFutureDate(),
                reason);
        }

        [Command("mute")]
        [Description("Permamutes a member")]
        [RequirePermissions(Permissions.BanMembers)]
        public async Task MuteMemberAsync(CommandContext ctx, [Description("Member to ban")] DiscordMember member, [Description("Y u do dis?!")] string reason = "") {
            await member.BanAsync(0, reason);
        }

        [Command("mute")]
        [Description("Temporary mute member")]
        [Aliases("tmute")]
        [RequirePermissions(Permissions.BanMembers)]
        public async Task MuteMemberAsync(CommandContext ctx, [Description("Member to ban")] DiscordMember member, [Description("Duration: \\d[mhdwMy]")] string duration = "1w", [Description("Y u do dis?!")] string reason = "") {
            await BanController.BanAsync(
                member,
                duration.ToFutureDate(),
                reason);
        }

        [Command("clear")]
        [Description("Removes messages\nA negative number will remove ALL messages INCLUDING pins (you've been warned).")]
        [RequirePermissions(Permissions.ManageMessages)]
        public async Task ClearAsync(CommandContext ctx, [Description("Messages to remove (max: 100)")] int amount = 50) {
            if(amount < 0) {
                var channel = await ctx.Channel.CloneAsync();
                await ctx.Channel.DeleteAsync();
                await channel.SendMessageAsync("All messages have been deleted.");
            } else if(amount <= 100) {
                try {
                    var messages = await ctx.Channel.GetMessagesBeforeAsync(ctx.Message.Id, amount);
                    await ctx.Channel.DeleteMessagesAsync(messages);
                } catch(Exception) {
                    await ctx.RespondAsync("No messages were deleted since some messages were over 14 days old." +
                        "\nThis is not a bug, but an API limitation.");
                }
                
            }
        }
    }
}
