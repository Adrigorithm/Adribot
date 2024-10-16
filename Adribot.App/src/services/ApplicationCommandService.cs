using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.Services.Providers;
using Discord.WebSocket;

namespace Adribot.Services;

public class ApplicationCommandService(DiscordClientProvider clientProvider)
{
    private async Task<IReadOnlyCollection<SocketApplicationCommand>> GetCommandsByNameAsync(IEnumerable<string> commandNames,
        ulong? guildId = null, bool includeGlobal = false)
    {
        async Task<IReadOnlyCollection<SocketApplicationCommand>> GetGlobalApplicationCommandsByName()
            => (await clientProvider.Client.GetGlobalApplicationCommandsAsync()).ToList().Where(c => commandNames.Contains(c.Name)).ToList();

        if (guildId is null)
            return await GetGlobalApplicationCommandsByName();
        
        SocketGuild? guild = clientProvider.Client.GetGuild(guildId.Value);

        List<SocketApplicationCommand> commands = guild is null
            ? []
            : (await guild.GetApplicationCommandsAsync()).ToList().Where(c => commandNames.Contains(c.Name)).ToList();

        if (includeGlobal)
            commands.AddRange(await GetGlobalApplicationCommandsByName());

        return commands;
    }

    private async Task<IReadOnlyCollection<SocketApplicationCommand>> GetAllCommandsAsync(ulong? guildId = null, bool includeGlobal = false)
    {
        if (guildId is null)
            return await clientProvider.Client.GetGlobalApplicationCommandsAsync();
        
        SocketGuild? guild = clientProvider.Client.GetGuild(guildId.Value);

        List<SocketApplicationCommand> commands = guild is null
            ? []
            : (await guild.GetApplicationCommandsAsync()).ToList();

        if (includeGlobal)
            commands.AddRange(await clientProvider.Client.GetGlobalApplicationCommandsAsync());

        return commands;
    }
}
