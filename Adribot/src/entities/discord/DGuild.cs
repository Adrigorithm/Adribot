using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Adribot.entities.discord;

[Table("dguilds")]
public class DGuild : IComparable
{
    [Key]
    [Column("dguildid")]
    public ulong DGuildId { get; set; }

    public List<DMember> Members { get; set; } = new();

    public List<DMember> GetMembersDifference(List<DMember> members)
    {
        members.Sort();
        Members.Sort();

        List<DMember> membersToAdd = new();
        for (int j = 0; j < members.Count; j++)
        {
            if (j > Members.Count - 1 || !Members[j].Equals(members[j]))
                membersToAdd.Add(members[j]);
        }

        return membersToAdd;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not DGuild guild || DGuildId != guild.DGuildId) return false;
        if (guild.Members.Count != Members.Count)
            return false;

        guild.Members.Sort();
        Members.Sort();

        return !Members.Where((m, i) => m != guild.Members[i]).Any();
    }
    
    public override int GetHashCode()
    {
        unchecked
        {
            return (DGuildId.GetHashCode() * 397) ^ Members.GetHashCode();
        }
    }

    public int CompareTo(object? obj)
    {
        if (obj is not DGuild guild)
            throw new InvalidCastException($"Object of type {obj?.GetType().Name} cannot be cast to {GetType().Name}");

        return DGuildId.CompareTo(guild.DGuildId);
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
