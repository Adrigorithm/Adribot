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

    public List<DMember> Members { get; set; } = new();

    public List<DMember> GetMembersDifference(List<DMember> members)
    {
        List<DMember> membersMissing = new();
        for (int i = 0; i < members.Count; i++)
        {
            if (Members.FirstOrDefault(m => m == members[i]) is null)
                membersMissing.Add(members[i]);
        }

        return membersMissing;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not DGuild guild || DGuildId != guild.DGuildId || guild.Members.Count != Members.Count)
            return false;

        return Members.Except(guild.Members).Count() == 0;
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
