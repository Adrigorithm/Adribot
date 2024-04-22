using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.services;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Adribot.src.commands.utilities;

public class RemoteAccessCommands(RemoteAccessService remoteAccess) : ApplicationCommandModule
{
    [SlashCommand("exec", "interact with a target server")]
    [SlashRequireOwner]
    public async Task ExecAsync(InteractionContext ctx, [Option("mode", "Type of action to execute")] ActionType action, [Option("guild", "Id of the guild to connect to")] long guildId = -1, [Option("channel", "channel id to connect to within current guild")] long channelId = -1, [Option("message", "text to send as a message")] string? message = null)
    {
        (bool, string?) result = await remoteAccess.ExecAsync(action, guildId == -1 ? null : (ulong)guildId, channelId == -1 ? null : (ulong)channelId, message);

        
    }
}
