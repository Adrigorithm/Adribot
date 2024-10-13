using System.Collections.Frozen;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adribot.Services.Providers;
using Discord.WebSocket;

namespace Adribot.Services;

public class StatisticsService(DiscordClientProvider clientProvider)
{
    /// <summary>
    /// Get all cached registered commands in a specific guild
    /// </summary>
    /// <param name="guild">Guild to search commands in</param>
    /// <returns>A dictionary where the key is the command name. Returns null if no commands were found in the guild.</returns>
    public async Task<FrozenDictionary<string, SocketApplicationCommand>> GetGuildCommandsAsync(SocketGuild guild)
    {
        IReadOnlyCollection<SocketApplicationCommand> commands = await guild.GetApplicationCommandsAsync();

        if (commands.Count == 0)
            return FrozenDictionary<string, SocketApplicationCommand>.Empty;

        return commands.ToFrozenDictionary(c => c.Name, c => c);
    }

    /// <summary>
    /// Get all cached registered commands in a specific guild
    /// </summary>
    /// <param name="guildId">Id of the guild to search commands in</param>
    /// <returns>A dictionary where the key is the command name. Returns null if no commands were found in the guild.</returns>
    public async Task<FrozenDictionary<string, SocketApplicationCommand>> GetGuildCommandsAsync(ulong guildId)
        => await GetGuildCommandsAsync(clientProvider.Client.GetGuild(guildId));

    public async Task<Dictionary<ulong?, FrozenDictionary<string, SocketApplicationCommand>>> GetAllCommandsAsync()
    {
        var commands = new Dictionary<ulong?, FrozenDictionary<string, SocketApplicationCommand>?> { [null!] = await GetGlobalCommandsAsync() };

        foreach (SocketGuild guild in clientProvider.Client.Guilds)
        {
            FrozenDictionary<string, SocketApplicationCommand> guildCommands = await GetGuildCommandsAsync(guild);

            if (guildCommands.Count > 0)
                commands[guild.Id] = guildCommands;
        }
        
        return commands;
    }

    public async Task<FrozenDictionary<string, SocketApplicationCommand>> GetGlobalCommandsAsync()
    {
        IReadOnlyCollection<SocketApplicationCommand> globalCommands = await clientProvider.Client.GetGlobalApplicationCommandsAsync();
        return globalCommands.ToFrozenDictionary(c => c.Name, c => c);
    }
}
