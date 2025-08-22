using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.constants.enums;
using Adribot.extensions;
using Adribot.services;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Adribot.commands.moderation;

public class AdminCommands(InfractionService infractionService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("clear", "Deletes given amount of messages")]
    [RequireUserPermission(ChannelPermission.ManageMessages)]
    [RequireBotPermission(ChannelPermission.ManageMessages)]
    public async Task DeleteMessagesAsync([Summary("Amount", "Amount of messages to delete")] [MinValue(1)] [MaxValue(100)] short amount)
    {
        IAsyncEnumerable<IReadOnlyCollection<IMessage>> messages = Context.Channel.GetMessagesAsync(amount);
        IEnumerable<IMessage> messagesFlattened = await messages.FlattenAsync();
        IEnumerable<IMessage> messagesToDelete = messagesFlattened.TakeWhile(m => m.Timestamp.AddDays(14) >= DateTimeOffset.Now);
        var oldMessages = amount - messagesToDelete.Count();

        await (Context.Channel as ITextChannel).DeleteMessagesAsync(messagesToDelete);

        var confirmMessage = oldMessages > 0
            ? $"Ink too dry! `{oldMessages}` of `{messagesFlattened.Count()}` message{(amount > 1 ? "s" : "")} could not be deleted."
            : $"Deleted {amount} Message{(amount > 1 ? "s" : "")}.";

        await RespondAsync(confirmMessage, ephemeral: true);
    }

    [SlashCommand("mute", "Mutes member using a Timeout")]
    [RequireUserPermission(GuildPermission.MuteMembers)]
    [RequireContext(ContextType.Guild)]
    public async Task MuteMemberAsync([Summary("Member", "Member to mute")] SocketGuildUser member, [Summary("Unit", "The duration multiplied by the factor parameter")] TimeSpanType type = TimeSpanType.Minutes, [Summary("Factor", "The amound of specified time units.")] [MinValue(1)] int factor = 3, [Summary("Reason", "The reason for this infraction")] string? reason = null)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        DateTimeOffset endDate = type.ToEndDate(factor, now);

        await member.SetTimeOutAsync(endDate - now);

        await RespondAsync();
    }

    [SlashCommand("ban", "Bans members")]
    [RequireUserPermission(GuildPermission.BanMembers)]
    [RequireContext(ContextType.Guild)]
    public async Task BanMemberAsync([Summary("Member", "Member to ban")] SocketGuildUser member, [Summary("Unit", "The duration multiplied by the factor parameter")] TimeSpanType type = TimeSpanType.Months, [Summary("Factor", "The amound of specified time units.")] [MinValue(1)] int factor = 1, [Summary("Messages", "Anount of messages by this user to delete")] int deleteMessages = 0, [Summary("Reason", "The reason for this infraction")] string? reason = null)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        DateTimeOffset endDate = type.ToEndDate(factor, now);

        infractionService.AddInfraction(Context.Guild.Id, member.Id, endDate, InfractionType.Ban, reason);

        await member.BanAsync(deleteMessages, reason);

        await RespondAsync();
    }
}
