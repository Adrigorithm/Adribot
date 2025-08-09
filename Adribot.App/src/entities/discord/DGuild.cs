using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.Data;
using Adribot.Entities.fun;
using Adribot.Entities.Utilities;
using Discord;

namespace Adribot.Entities.Discord;

public class DGuild : IComparable, IDataStructure
{
    public int DGuildId { get; set; }

    public ulong GuildId { get; set; }

    public Starboard Starboard { get; set; }

    public List<DMember> Members { get; set; } = [];

    public List<WireConfig> WireConfigs { get; set; } = [];

    public List<DMember> GetMembersDifference(List<DMember> members)
    {
        var membersUpdated = Members.ToHashSet();
        List<DMember> missingMembers = [];

        for (var i = 0; i < members.Count; i++)
        {
            if (membersUpdated.Add(members[i]))
                missingMembers.Add(members[i]);
        }

        return missingMembers;
    }

    /// <summary>
    /// Checks if two objects (most likely guilds - including their members) are equal.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>A boolean indicating whether the two objects are equal.</returns>
    public override bool Equals(object? obj) =>
        obj is DGuild guild && GuildId == guild.GuildId && guild.Members.Count == Members.Count && GetMembersDifference(guild.Members).Count == 0;


    public override int GetHashCode()
    {
        unchecked
        {
            return GuildId.GetHashCode() * 397 ^ Members.GetHashCode();
        }
    }

    public int CompareTo(object? obj)
    {
        if (obj is DGuild guild)
        {
            return guild.GuildId < GuildId ?
                1 : guild.GuildId > GuildId ? -1 : 0;
        }

        var typeName = obj is null ? "null" : obj.GetType().Name;
        throw new ArgumentException($"Instances of type {typeName} are not supported.\n" +
                                    $"Make sure your instance is of type {GetType().Name}");
    }

    public EmbedBuilder GenerateEmbedBuilder() =>
        new()
        {
            Author = new EmbedAuthorBuilder { Name = "<@608275633218519060>" },
            Title = GuildId.ToString(),
            Description = $"This guild contains {Members.Count} members."
        };
}
