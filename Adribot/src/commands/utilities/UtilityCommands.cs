using System;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.extensions;
using Adribot.src.services;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Adribot.src.commands.utilities;

public class UtilityCommands(RemindMeSerivce _remindMeService) : ApplicationCommandModule
{
    [SlashCommand("remindme", "Set an alert for something ahead of time")]
    [RequirePermissions(Permissions.SendMessages)]
    public async Task ExecuteRemindTaskAsync(InteractionContext ctx, [Option("task", "What you should be reminded of")] string taskTodo, [Option("unit", "Time unit to be muliplied by the next factor parameter")] TimeSpanType timeUnit, [Option("factor", "Amount of instances of the specified time unit")] long factor, [Option("channel", "Fallback for if you don't want the bot to dm you")] DiscordChannel altChannel = null)
    {
        DateTimeOffset now = DateTimeOffset.Now;
        DateTimeOffset endDate = timeUnit.ToEndDate((int)factor, now);

        if (endDate - now < TimeSpan.FromMinutes(1))
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                new DiscordMessageBuilder().WithContent($"Remind me's should be set at least 1 minute ahead in time.")).AsEphemeral());
        }
        else
        {
            _remindMeService.AddRemindMe(ctx.Guild.Id, ctx.Member.Id, altChannel?.Id ?? ctx.Channel.Id, taskTodo, endDate);

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
                new DiscordMessageBuilder().WithContent($"I will remind you at {endDate:g}")).AsEphemeral());
        }
    }
}
