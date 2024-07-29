using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.services;
using Discord.Interactions;

namespace Adribot.src.commands.utilities;

public class RemoteAccessCommands(RemoteAccessService remoteAccess) : InteractionModuleBase
{
    [SlashCommand("exec", "interact with a target server")]
    [RequireOwner]
    public async Task ExecAsync([Summary("mode", "Type of action to execute")] RemoteAccessActionType action, [Summary("guild", "Id of the guild to connect to")] ulong? guildId = null, [Summary("channel", "channel id to connect to within current guild")] ulong? channelId = null, [Summary("message", "text to send as a message")] string? message = null)
    {
        (bool, string?) result = await remoteAccess.ExecAsync(action, guildId, channelId, message);

        await RespondAsync($"Remote action {(result.Item1 ? "**succeeded**" : "**failed**")} with message:\n`{result.Item2 ?? "No message provided"}`", ephemeral: true);
    }
}
