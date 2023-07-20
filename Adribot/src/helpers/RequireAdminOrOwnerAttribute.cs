using Adribot.config;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace Adribot.src.helpers
{
    public class RequirePermissionOrOwnerAttribute : SlashCheckBaseAttribute
    {
        private readonly Permissions _permission;

        public RequirePermissionOrOwnerAttribute(Permissions permission) =>
            _permission = permission;

        public override Task<bool> ExecuteChecksAsync(InteractionContext ctx) =>
            Task.FromResult(ctx.Member.Permissions.HasPermission(_permission) || ctx.User.Id == Config.Configuration.DevUserId);
    }
}
