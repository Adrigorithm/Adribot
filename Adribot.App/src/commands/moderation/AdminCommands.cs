using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.extensions;
using Adribot.src.services;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Adribot.src.commands.moderation;

public class AdminCommands(InfractionService _infractionService) : InteractionModuleBase
{
    [SlashCommand("clear", "Deletes given amount of messages")]
    [RequireUserPermission(ChannelPermission.ManageMessages)]
    public async Task DeleteMessagesAsync([Summary("Amount", "Amount of messages to delete"), MinValue(1), MaxValue(100)] short amount)
    {
        IAsyncEnumerable<IReadOnlyCollection<IMessage>> messages = Context.Channel.GetMessagesAsync(amount);
        IEnumerable<IMessage> messagesFlattened = await messages.FlattenAsync();

        await (Context.Channel as ITextChannel).DeleteMessagesAsync(messagesFlattened);

        IMessage? oldMessage = await Context.Channel.GetMessageAsync(messagesFlattened.Last().Id);
        var confirmMessage = oldMessage is not null
            ? $"Ink too dry! Some messages could not be deleted."
            : $"Deleted {messagesFlattened.Count()} Message{(messagesFlattened.Count() > 1 ? "s" : "")}.";

        await RespondAsync(confirmMessage.ToString(), ephemeral: true);
    }

    [SlashCommand("mute", "Mutes member using a Timeout")]
    [RequireUserPermission(GuildPermission.MuteMembers)]
    [RequireContext(ContextType.Guild)]
    public async Task MuteMemberAsync([Summary("Member", "Member to mute")] SocketGuildUser member, [Summary("Unit", "The duration multiplied by the factor parameter")] TimeSpanType type = TimeSpanType.Minutes, [Summary("Factor", "The amound of specified time units."), MinValue(1)] int factor = 3, [Summary("Reason", "The reason for this infraction")] string? reason = null)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        DateTimeOffset endDate = type.ToEndDate(factor, now);

        await member.SetTimeOutAsync(endDate - now);
    }

    [SlashCommand("ban", "Bans members")]
    [RequireUserPermission(GuildPermission.BanMembers)]
    [RequireContext(ContextType.Guild)]
    public async Task BanMemberAsync([Summary("Member", "Member to ban")] SocketGuildUser member, [Summary("Unit", "The duration multiplied by the factor parameter")] TimeSpanType type = TimeSpanType.Months, [Summary("Factor", "The amound of specified time units."), MinValue(1)] int factor = 1, [Summary("Messages", "Anount of messages by this user to delete")] int deleteMessages = 0, [Summary("Reason", "The reason for this infraction")] string? reason = null)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        DateTimeOffset endDate = type.ToEndDate(factor, now);

        _infractionService.AddInfraction(Context.Guild.Id, member.Id, endDate, InfractionType.Ban, reason);

        await member.BanAsync(deleteMessages, reason);
    }
}
