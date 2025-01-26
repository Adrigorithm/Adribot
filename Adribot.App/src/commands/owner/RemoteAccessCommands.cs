using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Services;
using Discord.Interactions;

namespace Adribot.Commands.Owner;

public class RemoteAccessCommands(RemoteAccessService remoteAccess) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("exec", "interact with a target server")]
    [RequireOwner]
    public async Task ExecAsync([Summary("mode", "Type of action to execute")] RemoteAccessActionType action, [Summary("guild", "Id of the guild to connect to")] string? guildId = null, [Summary("channel", "channel id to connect to within current guild")] string? channelId = null, [Summary("message", "text to send as a message")] string? message = null)
    {
        _ = ulong.TryParse(guildId, out var guildIdParsed);
        _ = ulong.TryParse(channelId, out var channelIdParsed);
        
        (bool, string?) result = await remoteAccess.ExecAsync(action, guildIdParsed, channelIdParsed, message);

        await RespondAsync($"Remote action {(result.Item1 ? "**succeeded**" : "**failed**")} with message:\n`{result.Item2 ?? "No message provided"}`", ephemeral: true);
    }
}
