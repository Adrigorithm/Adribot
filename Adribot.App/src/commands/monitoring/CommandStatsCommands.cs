using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Services;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Adribot.Commands.Monitoring;

public class CommandStatsCommands(ApplicationCommandService commandService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("stats", "retrieve stats for a specific metric")]
    [RequireUserPermission(ChannelPermission.SendMessages)]
    public async Task GetStatsAsync([Summary("metric", "The name of the metric you want to retrieve")] MonitoringOptions metric = MonitoringOptions.AllCommands, [Summary("guildId", "The guild to get statistics from")] string? guildId = null)
    {
        ImmutableArray<SocketApplicationCommand> guildCommands;
        ImmutableArray<SocketApplicationCommand> globalCommands;
        ulong guildIdParsed;

        switch (metric)
        {
            case MonitoringOptions.GuildCommands:
                if (!ulong.TryParse(guildId, out guildIdParsed))
                    await RespondAsync("Cannot search by guild without a guild ID.", ephemeral: true);

                guildCommands = [.. await commandService.GetAllCommandsAsync(guildIdParsed)];

                if (guildCommands.Length == 0)
                    await RespondAsync($"No commands found in guild with ID {guildId}", ephemeral: true);

                await RespondAsync(CommandListString(guildCommands, false, guildIdParsed));

                break;
            case MonitoringOptions.GlobalCommands:
                globalCommands = [.. await commandService.GetAllCommandsAsync()];

                if (globalCommands.Length == 0)
                    await RespondAsync("No commands found.", ephemeral: true);

                await RespondAsync(CommandListString(globalCommands));

                break;
            case MonitoringOptions.AllCommands:
                var conversionSucceeded = ulong.TryParse(guildId, out guildIdParsed);

                IReadOnlyCollection<SocketApplicationCommand> commands = await commandService.GetAllCommandsAsync(guildIdParsed, true);

                if (commands.Count == 0)
                    await RespondAsync("No commands found.", ephemeral: true);

                guildCommands = !conversionSucceeded
                    ? []
                    : [.. commands.TakeWhile(c => !c.IsGlobalCommand)];

                globalCommands = guildCommands.Length == commands.Count
                    ? []
                    : [.. commands.Skip(guildCommands.Length)];

                var sb = new StringBuilder();

                if (guildCommands.Length > 0)
                    sb.AppendLine(CommandListString(guildCommands, false, guildIdParsed));

                if (globalCommands.Length > 0)
                    sb.AppendLine(CommandListString(globalCommands));

                await RespondAsync(sb.ToString());

                break;
            default:
                await RespondAsync("Metric not implemented", ephemeral: true);

                break;
        }
    }

    /// <summary>
    /// Create a string representation of a list of commands. This is a shortened list intended to send through discord or terminal.
    /// </summary>
    /// <param name="commands"></param>
    /// <param name="isGlobal"></param>
    /// <param name="guildId">Guild ID to set if the commands are guild commands</param>
    /// <returns>A nicely formatted string of command names</returns>
    private string CommandListString(IReadOnlyCollection<SocketApplicationCommand> commands, bool isGlobal = true, ulong? guildId = null)
    {
        StringBuilder sb = isGlobal
            ? new StringBuilder($"{Environment.NewLine}Global Commands (`{commands.Count}`):{Environment.NewLine}")
            : new StringBuilder($"{Environment.NewLine}Guild `{guildId}` Commands (`{commands.Count}`):{Environment.NewLine}");

        commands.ToImmutableList().ForEach(c => sb.Append($"`{c.Name}` "));

        return sb.ToString();
    }
}
