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
    }
}
