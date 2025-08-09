using System.Threading.Tasks;
using Adribot.Services;
using Discord.Interactions;

namespace Adribot.Commands.Utilities;

public class WireCommands(WireService wireService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("register-emote", "Registers an emote for future use")]
    public async Task ExecuteRemindTaskAsync([Summary("emote", "The emote you wish to register")] string emoteString, [Summary("guild", "The ID of the guild the on which the emote should be registered")] string guildId, [Summary("config", "Name of this configuration")] string name)
    {
        _ = ulong.TryParse(guildId, out var guildIdParsed);

        (bool isSuccess, string error) = await wireService.TryCreateWireConfigAsync(guildIdParsed, Context.User.Id, name, emoteString);

        if (isSuccess)
            await RespondAsync($"Successfully registered emote", ephemeral: true);
        else
            await RespondAsync($"Could not register emote: {error}", ephemeral: true);
    }

    [SlashCommand("generate-emote", "Generates a registered emote")]
    public async Task CreateWireAsync([Summary("name", "The name of the config you wish to re-generate the emote for")] string name, [Summary("replace", "Whether live emote for this config should be replaced")] bool shouldReplace = true)
    {
        (bool, string) result = await wireService.CreateWireAsync(Context.User.Id, name, shouldReplace);

        if (result.Item1)
            await RespondAsync("Emote regenerated!", ephemeral: true);
        else
            await RespondAsync($"Could not regenerate emote: {result.Item2}", ephemeral: true);
    }
}
