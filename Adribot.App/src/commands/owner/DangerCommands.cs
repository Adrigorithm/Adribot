using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Services;
using Discord.Interactions;

namespace Adribot.Commands.Owner;

public class DangerCommands(ApplicationCommandService commandService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("command", "manage registered commands")]
    [RequireOwner]
    public async Task ExecAsync([Summary("mode", "Type of action to execute")] CrudOperation action,
        [Summary("name", "name of the command")] string name,
        [Summary("guild", "Id of the guild to manage commands for")] string? guildId = null)
    {
        switch (action)
        {
            case CrudOperation.Delete:
                _ = ulong.TryParse(guildId, out var guildIdParsed);
                var deleted = await commandService.UnregisterCommandAsync(name, guildIdParsed);

                await RespondAsync(deleted
                    ? $"Unregistered command **{name}**"
                    : $"Failed to unregister command **{name}**", ephemeral: true);
                break;
            default:
                await RespondAsync($"Action {action.ToString()} is not a valid action for this command.");
                break;
        }
    }
}
