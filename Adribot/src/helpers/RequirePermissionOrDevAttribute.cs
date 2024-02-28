using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace Adribot.src.helpers;

public class RequirePermissionOrDevAttribute : SlashCheckBaseAttribute
{
    private readonly Permissions _permission;
    private readonly ulong _devUserId;

    public RequirePermissionOrDevAttribute(ulong devUserId, Permissions permission) =>
        (_permission, _devUserId) = (permission, devUserId);

    public override Task<bool> ExecuteChecksAsync(InteractionContext ctx) =>
        Task.FromResult(ctx.Member.Permissions.HasPermission(_permission) || ctx.User.Id == _devUserId);
}