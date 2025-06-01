using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.Services.Providers;
using Discord.WebSocket;
using Microsoft.IdentityModel.Tokens;

namespace Adribot.Services;

public class ApplicationCommandService(DiscordClientProvider clientProvider)
{
    /// <summary>
    /// Unregisters an application command
    /// </summary>
    /// <param name="commandName">Name of the command to delete</param>
    /// <param name="guildId">Specify the guild ID within the guild command should be deleted</param>
    /// <returns></returns>
    public async Task<bool> UnregisterCommandAsync(string commandName, ulong? guildId = null)
    {
        if (commandName.IsNullOrEmpty())
            return false;

        SocketApplicationCommand? command = (await GetCommandsByNameAsync([commandName], guildId)).FirstOrDefault();

        if (command is null)
            return false;

        await command.DeleteAsync();

        return true;
    }

    private async Task<IReadOnlyCollection<SocketApplicationCommand>> GetCommandsByNameAsync(IEnumerable<string> commandNames,
        ulong? guildId = null, bool includeGlobal = false)
    {
        async Task<IReadOnlyCollection<SocketApplicationCommand>> GetGlobalApplicationCommandsByName()
            => (await clientProvider.Client.GetGlobalApplicationCommandsAsync()).ToList().Where(c => commandNames.Contains(c.Name)).ToList();

        if (guildId is null or 0)
            return await GetGlobalApplicationCommandsByName();

        SocketGuild? guild = clientProvider.Client.GetGuild(guildId.Value);

        List<SocketApplicationCommand> commands = guild is null
            ? []
            : (await guild.GetApplicationCommandsAsync()).ToList().Where(c => commandNames.Contains(c.Name)).ToList();

        if (includeGlobal)
            commands.AddRange(await GetGlobalApplicationCommandsByName());

        return commands;
    }

    public async Task<IReadOnlyCollection<SocketApplicationCommand>> GetAllCommandsAsync(ulong? guildId = null, bool includeGlobal = false)
    {
        if (guildId is null or 0)
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
