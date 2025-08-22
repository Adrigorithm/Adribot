using System;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Adribot.helpers;

public class RequirePermissionOrUserAttribute : PreconditionAttribute
{
    private readonly ulong _devUserId;
    private readonly ChannelPermission _permission;

    public RequirePermissionOrUserAttribute(ulong devUserId, ChannelPermission permission) =>
        (_permission, _devUserId) = (permission, devUserId);

    public override async Task<PreconditionResult> CheckRequirementsAsync(IInteractionContext context, ICommandInfo commandInfo, IServiceProvider services)
    {
#pragma warning disable IDE0046 // Convert to conditional expression
        if (context.User is not SocketGuildUser member)
            return await Task.FromResult(PreconditionResult.FromError("You are not in a guild!"));
#pragma warning restore IDE0046 // Convert to conditional expression

        return member.Id == _devUserId || member.GetPermissions(context.Channel as IGuildChannel).Has(_permission)
            ? await Task.FromResult(PreconditionResult.FromSuccess())
            : await Task.FromResult(PreconditionResult.FromError("Insufficient permissions!"));
    }
}
