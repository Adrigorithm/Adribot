using System.Threading.Tasks;
using Adribot.Services;
using Discord;
using Discord.Interactions;

namespace Adribot.Commands.Monitoring;

public class CommandStatsCommands(StatisticsService statisticsService) : InteractionModuleBase<SocketInteractionContext>
{
    // [SlashCommand("commands", "retrieve stats for the registered application commands")]
    // [RequireUserPermission(ChannelPermission.SendMessages)]
    // public async Task GetCommandStatsAsync()
    // {
    //     (bool, string?) result = await remoteAccess.ExecAsync(action, guildId, channelId, message);
    //
    //     await RespondAsync($"Remote action {(result.Item1 ? "**succeeded**" : "**failed**")} with message:\n`{result.Item2 ?? "No message provided"}`", ephemeral: true);
    // }
}
