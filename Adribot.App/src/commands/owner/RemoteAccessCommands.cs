using System.Threading.Tasks;
using Adribot.src.services;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Adribot.src.commands.utilities;

public class RemoteAccessCommands(RemoteAccessService remoteAccess) : ApplicationCommandModule
{
    [SlashCommand("attach", "Creates an interactive session with a target server")]
    [SlashRequireOwner]
    public async Task AttachAsync(InteractionContext ctx, [Option("guild", "Id of the guild to connect to")] long id, [Option("channel", "The specific channel to listen to (defaults to all)")] DiscordChannel channel = null)
    {
        remoteAccess.Attach((ulong)id, channel?.Id);
        
        await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"Couldn't connect to guild `{id}`, make sure the bot can read it.")).AsEphemeral());
    }

    [SlashCommand("detach", "Closes an interactive session for a target server")]
    [SlashRequireOwner]
    public async Task DetachAsync(InteractionContext ctx)
    {
        remoteAccess.Detach();
        
        await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent($"Detached from current guild")).AsEphemeral());
    }
}
