using System.Threading.Tasks;
using Adribot.src.config;
using DSharpPlus;
using DSharpPlus.SlashCommands;

namespace Adribot.src.helpers
{
    public class RequirePermissionOrDevAttribute : SlashCheckBaseAttribute
    {
        private readonly Permissions _permission;

        public RequirePermissionOrDevAttribute(Permissions permission) =>
            _permission = permission;

        public override Task<bool> ExecuteChecksAsync(InteractionContext ctx) =>
            Task.FromResult(ctx.Member.Permissions.HasPermission(_permission) || ctx.User.Id == Config.Configuration.DevUserId);
    }
}
