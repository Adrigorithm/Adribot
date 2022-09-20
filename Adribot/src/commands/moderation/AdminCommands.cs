using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

class AdminCommands : ApplicationCommandModule
{
    [SlashCommand("Clear", "Deletes given amount of messages")]
    [SlashRequirePermissions(Permissions.ManageMessages)]
    public async Task DeleteMessagesAsync(InteractionContext ctx, [Option("Amount", "Amount of messages to delete"), Minimum(2), Maximum(100)] long amount = 10)
    {
        var messages = await ctx.Channel.GetMessagesAsync((int)amount);
        int index = messages.Count - 1;
        while (index > -1)
        {
            if (messages[index].Timestamp.UtcDateTime.AddDays(14).CompareTo(DateTime.UtcNow) >= 0)
            {
                break;
            }
            index--;
        }

        if(index > -1) await ctx.Channel.DeleteMessagesAsync(messages.Take(index + 1));
        if (index < messages.Count - 1)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"Ink too dry! {messages.Count - index - 1} Messages could not be deleted.").AsEphemeral(true));
        }
        else
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"Deleted {messages.Count} Messages."));
        }
    }

    [SlashCommand("Mute", "Mutes member using a Timeout")]
    [SlashRequirePermissions(Permissions.MuteMembers)]
    public async Task MuteMemberAsync(InteractionContext ctx, [Option("Member", "Member to mute")] DiscordUser member, [Option("Unit", "The duration multiplied by the factor parameter")] TimeSpanType type = TimeSpanType.MINUTES, [Option("Factor", "The amound of specified time units."), Minimum(1)] long factor = 3, [Option("Reason", "The reason for this infraction")] string reason = ""){
        var now = DateTimeOffset.UtcNow;
        var endDate = new KeyValuePair<TimeSpanType, long>(type, factor).ToEndDate(now);
        await TimerServiceProvider.AddDataAsync(new Infraction{
            Date = now,
            EndDate = endDate,
            GuildId = ctx.Guild.Id,
            isExpired = false,
            MemberId = member.Id,
            Type = InfractionType.TIMEOUT
        });
        await ((DiscordMember)member).TimeoutAsync(endDate, reason);
    }

    [SlashCommand("Ban", "Bans members")]
    [SlashRequirePermissions(Permissions.BanMembers)]
    public async Task BanMemberAsync(InteractionContext ctx, [Option("Member", "Member to ban")] DiscordUser member, [Option("Unit", "The duration multiplied by the factor parameter")] TimeSpanType type = TimeSpanType.MONTHS, [Option("Factor", "The amound of specified time units."), Minimum(1)] long factor = 1, [Option("Messages", "Anount of messages by this user to delete")] long deleteMessages = 0, [Option("Reason", "The reason for this infraction")] string reason = ""){
        var now = DateTimeOffset.UtcNow;
        var endDate = new KeyValuePair<TimeSpanType, long>(type, factor).ToEndDate(now);
        await TimerServiceProvider.AddDataAsync(new Infraction{
            Date = now,
            EndDate = endDate,
            GuildId = ctx.Guild.Id,
            isExpired = false,
            MemberId = member.Id,
            Type = InfractionType.BAN
        });
        await ((DiscordMember)member).BanAsync(Convert.ToInt16(deleteMessages), reason);
    }
}
