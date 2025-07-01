using System.Threading.Tasks;
using Adribot.Services;
using Discord.Interactions;

namespace Adribot.Commands.Utilities;

public class WireCommands(WireService wireService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("register-emote", "Registers an emote for future use")]
    public async Task ExecuteRemindTaskAsync([Summary("emote", "The emote you wish to register")] string emoteString, [Summary("guild", "The ID of the guild the on which the emote should be registered")] ulong guildId, [Summary("config", "Name of this configuration")] string name)
    {
        var (isSuccess, error) = await wireService.TryCreateWireConfigAsync(guildId, name, emoteString);

        if (isSuccess)
            await RespondAsync($"Successfully registered emoji", ephemeral: true);
        else
            await RespondAsync($"Could not register emote: {error}", ephemeral: true);
    }
}
