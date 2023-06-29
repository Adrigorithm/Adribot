using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.entities.discord;
using DSharpPlus.Entities;

namespace Adribot.extensions;

public static class DiscordObjectExtensions{
    public static async Task<DGuild> ToDGuildAsync(this DiscordGuild guild, bool includeMembers = true) =>
        new DGuild 
        {
            DGuildId = guild.Id,
            Members = !includeMembers ? new() : (await guild.GetAllMembersAsync()).ToDMembers() 
        };

    public static DMember ToDMember(this DiscordMember member) =>
        new DMember
        {
            DGuildId = member.Guild.Id,
            DMemberId = member.Id
        };

    public static async Task<List<DGuild>> ToDGuildsAsync(this IEnumerable<DiscordGuild> guilds, bool includeMembers = true){
        List<DGuild> dGuilds = new();

        for (int i = 0; i < guilds.Count(); i++)
        {
            dGuilds.Add(await guilds.ElementAt(i).ToDGuildAsync(includeMembers));
        }

        return dGuilds;
    }

    public static List<DMember> ToDMembers(this IEnumerable<DiscordMember> members)
    {
        List<DMember> dMembers = new();

        members.ToList().ForEach(m => dMembers.Add(m.ToDMember()));

        return dMembers;
    }
}
