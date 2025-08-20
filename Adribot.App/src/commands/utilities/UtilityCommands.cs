using System;
using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Extensions;
using Adribot.Services;
using Discord;
using Discord.Interactions;

namespace Adribot.Commands.Utilities;

public class UtilityCommands(RemindMeService remindMeService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("remindme", "Set an alert for something ahead of time")]
    public async Task ExecuteRemindTaskAsync([Summary("task", "What you should be reminded of")] string taskTodo, [Summary("unit", "Time unit to be muliplied by the next factor parameter")] TimeSpanType timeUnit, [Summary("factor", "Amount of instances of the specified time unit")] int factor, [Summary("channel", "Fallback for if you don't want the bot to dm you")] ITextChannel? altChannel = null)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        DateTimeOffset endDate = timeUnit.ToEndDate(factor, now);

        if (endDate - now < TimeSpan.FromMinutes(1))
            await RespondAsync("Remind me's should be set at least 1 minute ahead in time.", ephemeral: true);
        else
        {
            remindMeService.AddRemindMe(Context.Guild.Id, Context.User.Id, altChannel?.Id, taskTodo, endDate);

            await RespondAsync($"I will remind you {new TimestampTag(endDate, TimestampTagStyles.Relative)}", ephemeral: true);
        }
    }
}
