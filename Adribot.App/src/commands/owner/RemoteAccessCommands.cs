using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.services;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Adribot.src.commands.utilities;

public class RemoteAccessCommands(RemoteAccessService remoteAccess) : ApplicationCommandModule
{
    [SlashCommand("exec", "interact with a target server")]
    [SlashRequireOwner]
    public async Task ExecAsync(InteractionContext ctx, [Option("mode", "Type of action to execute")] ActionType action, [Option("guild", "Id of the guild to connect to")] string? guildId = null, [Option("channel", "channel id to connect to within current guild")] string? channelId = null, [Option("message", "text to send as a message")] string? message = null)
    {
        (bool, string?) result = await remoteAccess.ExecAsync(action, guildId is null ? null : ulong.Parse(guildId), channelId is null ? null : ulong.Parse(channelId), message);

        await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"Remote action {(result.Item1 ? "**succeeded**" : "**failed**")} with message:\n`{result.Item2 ?? "No message provided"}`")).AsEphemeral());
    }
}
