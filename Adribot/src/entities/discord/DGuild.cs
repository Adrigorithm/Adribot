using Adribot.src.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Adribot.entities.discord;

public class DGuild : IComparable, IDataStructure
{
    [Key]
    public ulong DGuildId { get; set; }

    public ulong? StarboardChannel { get; set; }
    public string StarEmoji { get; set; }

    public virtual List<DMember> Members { get; set; } = new();

    public List<DMember> GetMembersDifference(List<DMember> members)
    {
        var membersUpdated = Members.ToHashSet();
        List<DMember> missingMembers = new();

        for (int i = 0; i < members.Count; i++)
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
        obj is DGuild guild && DGuildId == guild.DGuildId && guild.Members.Count == Members.Count && GetMembersDifference(guild.Members).Count == 0;


    public override int GetHashCode()
    {
        unchecked
        {
            return (DGuildId.GetHashCode() * 397) ^ Members.GetHashCode();
        }
    }

    public int CompareTo(object? obj)
    {
        if (obj is DGuild guild)
            return guild.DGuildId < DGuildId ?
                1 : guild.DGuildId > DGuildId ? -1 : 0;

        string typeName = obj is null ? "null" : obj.GetType().Name;
        throw new ArgumentException($"Instances of type {typeName} are not supported.\n" +
                                    $"Make sure your instance is of type {GetType().Name}");
    }

    public override string ToString()
    {
        var sb = new StringBuilder($"-- Guild[{DGuildId}: {Members.Count}] --{Environment.NewLine}");

        for (int i = 1; i <= Members.Count; i++)
        {
            sb.AppendLine($"Member {i}: {Members[i - 1].DMemberId}");
        }

        return sb.ToString();
    }
}
