using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.entities.discord;
using DSharpPlus.Entities;

namespace Adribot.src.extensions;

public static class DiscordObjectExtensions
{
    public static async Task<DGuild> ToDGuildAsync(this DiscordGuild guild, bool includeMembers = true) =>
        new DGuild
        {
            DGuildId = guild.Id,
            Members = !includeMembers ? new() : await guild.GetAllMembersAsync().ToDMembersAsync(guild.Id)
        };

    /// <summary>
    /// Calling this method method individually will NOT reference to a DGuild in any way.
    /// </summary>
    /// <param name="member">The member to be converted</param>
    /// <returns>The simplified DTO (DMember) representation of a DiscordMember</returns>
    public static DMember ToDMember(this DiscordMember member, ulong guildId) =>
        new DMember
        {
            DMemberId = member.Id,
            DGuildId = guildId
        };

    public static async Task<List<DGuild>> ToDGuildsAsync(this IEnumerable<DiscordGuild> guilds, bool includeMembers = true)
    {
        List<DGuild> dGuilds = new();

        for (var i = 0; i < guilds.Count(); i++)
        {
            dGuilds.Add(await guilds.ElementAt(i).ToDGuildAsync(includeMembers));
        }

        return dGuilds;
    }

    public static async Task<List<DMember>> ToDMembersAsync(this IAsyncEnumerable<DiscordMember> members, ulong guildId)
    {
        List<DMember> dMembers = new();

        await foreach (DiscordMember m in members)
            dMembers.Add(m.ToDMember(guildId));

        return dMembers;
    }

    public static List<DMember> ToDMembers(this IEnumerable<DiscordMember> members, ulong guildId)
    {
        List<DMember> dMembers = new();

        members.ToList().ForEach(m => dMembers.Add(m.ToDMember(guildId)));

        return dMembers;
    }
}
