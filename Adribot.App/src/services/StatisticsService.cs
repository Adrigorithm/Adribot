using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.services.providers;
using Discord.WebSocket;

namespace Adribot.src.services;

public class StatisticsService(DiscordClientProvider clientProvider)
{
    /// <summary>
    /// Get all cached registered commands from the current user
    /// </summary>
    /// <param name="guild">Guild to search commands in</param>
    /// <returns>A dictionary where the key indicates the availability of the command. Returns null if no commands were found in the guild.</returns>
    public async Task<FrozenDictionary<CommandScope, SocketApplicationCommand>?> GetCommandsByGuildAsync(SocketGuild guild)
    {
        IReadOnlyCollection<SocketApplicationCommand> commands = await guild.GetApplicationCommandsAsync();

        if (commands.Count == 0)
            return null;

        return commands.ToFrozenDictionary(c => c.IsGlobalCommand
                ? CommandScope.Global
                : CommandScope.Guild
            , c => c);
    }

    /// <summary>
    /// Get all cached registered commands from the current user
    /// </summary>
    /// <param name="guildId">Id of the guild to search commands in</param>
    /// <returns>A dictionary where the key indicates the availability of the command. Returns null if no commands were found in the guild.</returns>
    public async Task<FrozenDictionary<CommandScope, SocketApplicationCommand>?> GetCommandsByGuildAsync(ulong guildId)
        => await GetCommandsByGuildAsync(clientProvider.Client.GetGuild(guildId));
}
