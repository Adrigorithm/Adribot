using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.entities.utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.discord;

[Table("dmembers")]
[PrimaryKey(nameof(DMemberId), nameof(DGuildId))]
public class DMember : IComparable
{
    [Column("dmemberid")]
    public ulong DMemberId { get; set; }

    public List<Infraction> Infractions { get; set; } = new();
    public List<Reminder> Reminders { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();

    [ForeignKey("dguildid")]
    public ulong DGuildId { get; set; }
    public DGuild DGuild { get; set; }

    public int CompareTo(object? obj)
    {
        if (obj is DMember member)
            return member.DMemberId < DMemberId ? 
                1 : member.DMemberId > DMemberId ? -1 : 0;

        string typeName = obj is null ? "null" : obj.GetType().Name;
        throw new ArgumentException($"Instances of type {typeName} are not supported.\n" +
                                    $"Make sure your instance is of type {GetType().Name}");
    }

    public virtual bool Equals(DMember? other)
    {
        if (other is not DMember member) return false;
        return member.DMemberId == DMemberId;
    }

    public override int GetHashCode() => DMemberId.GetHashCode();
}
