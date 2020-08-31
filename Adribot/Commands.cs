using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Adribot
{
    public class Commands
    {
        [Command("clear"), Description("Clears chat history"), RequirePermissions(Permissions.ManageMessages)]
        public async Task ClearChat(CommandContext ctx, [Description("Amount of messages to be deleted.")] int amount = -1) {
            
        }

        [Command("ban"), Description("Ban a player"), RequirePermissions(Permissions.BanMembers)]
        public async Task BanMember(CommandContext ctx, [Description("Member to ban.")] DiscordMember member, [Description("Reason")] string reason = "You've been banned.") {
            await ctx.Guild.BanMemberAsync(member, 0, reason);
        }
    }
}
