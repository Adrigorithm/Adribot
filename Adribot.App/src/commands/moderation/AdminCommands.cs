using System;
using System.Text;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.extensions;
using Adribot.src.helpers;
using Adribot.src.services;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Adribot.src.commands.moderation;

public class AdminCommands(InfractionService _infractionService) : ApplicationCommandModule
{
    [SlashCommand("Clear", "Deletes given amount of messages")]
    [RequirePermissionOrDev(135081249017430016, Permissions.ManageMessages)]
    public async Task DeleteMessagesAsync(InteractionContext ctx, [Option("Amount", "Amount of messages to delete"), Minimum(1), Maximum(100)] long amount)
    {
        var deletedMessages = await ctx.Channel.DeleteMessagesAsync(ctx.Channel.GetMessagesAsync((int)amount));

        var oldMessages = amount - deletedMessages;
        StringBuilder confirmMessage = new();

        if (oldMessages > 0)
            confirmMessage.AppendLine($"Ink too dry! {oldMessages} Message{(oldMessages > 1 ? "s" : "")} could not be deleted.");
        confirmMessage.AppendLine($"Deleted {deletedMessages} Message{(deletedMessages > 1 ? "s" : "")}.");

        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent(confirmMessage.ToString()).AsEphemeral(true));
    }

    [SlashCommand("Mute", "Mutes member using a Timeout")]
    [SlashRequirePermissions(Permissions.MuteMembers)]
    public async Task MuteMemberAsync(InteractionContext ctx, [Option("Member", "Member to mute")] DiscordUser member, [Option("Unit", "The duration multiplied by the factor parameter")] TimeSpanType type = TimeSpanType.MINUTES, [Option("Factor", "The amound of specified time units."), Minimum(1)] long factor = 3, [Option("Reason", "The reason for this infraction")] string reason = "")
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        DateTimeOffset endDate = type.ToEndDate((int)factor, now);

        await ((DiscordMember)member).TimeoutAsync(endDate, reason);
    }

    [SlashCommand("Ban", "Bans members")]
    [SlashRequirePermissions(Permissions.BanMembers)]
    public async Task BanMemberAsync(InteractionContext ctx, [Option("Member", "Member to ban")] DiscordUser member, [Option("Unit", "The duration multiplied by the factor parameter")] TimeSpanType type = TimeSpanType.MONTHS, [Option("Factor", "The amound of specified time units."), Minimum(1)] long factor = 1, [Option("Messages", "Anount of messages by this user to delete")] long deleteMessages = 0, [Option("Reason", "The reason for this infraction")] string reason = "")
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        DateTimeOffset endDate = type.ToEndDate((int)factor, now);

        _infractionService.AddInfraction(ctx.Guild.Id, ctx.Member.Id, endDate, InfractionType.BAN, reason);

        await ((DiscordMember)member).BanAsync(Convert.ToInt16(deleteMessages), reason);
    }
}
