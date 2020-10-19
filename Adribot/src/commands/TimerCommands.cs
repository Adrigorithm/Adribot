using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;

namespace Adribot.commands
{
    class TimerCommands : BaseCommandModule
    {
        [Command("ban"), Description("Bans members"), RequirePermissions(Permissions.BanMembers)]
        public async Task Ban(CommandContext ctx, [Description("Member to be banned")] DiscordMember member, [Description("Timespan, defaults to infinite")] string timespan = "infinite", [Description("Reason")] string reason = null) {
            if(timespan.Equals("infinite")) {
                await ctx.Guild.BanMemberAsync(member, 0, reason);
            } else {
                DateTime banExpire = DateTime.Now;
                double quantity = double.Parse(timespan[0..^1]);

                switch(char.ToUpper(timespan[^1])) {
                    case 'H':
                        banExpire.AddHours(quantity);
                        break;
                    case 'D':
                        banExpire.AddDays(quantity);
                        break;
                    case 'W':
                        banExpire.AddDays(7 * quantity);
                        break;
                    case 'M':
                        banExpire.AddMonths((int)quantity);
                        break;
                    case 'Y':
                        banExpire.AddYears((int)quantity);
                        break;
                    default:
                        await ctx.RespondAsync("Invalid timespan specified, valid options are:\n `H`(Hours) `D`(Days) `W`(Weeks) `M`(Months) `Y`(Years)");
                        return;
                }
            }
        }
    }
}
