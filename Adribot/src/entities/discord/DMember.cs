using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.entities.utilities;
using Adribot.src.data;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.discord;

[PrimaryKey(nameof(DGuildId), nameof(DMemberId))]
public class DMember : IComparable, IDataStructure
{
    public ulong DMemberId { get; set; }

    public ulong DGuildId { get; set; }
    [ForeignKey(nameof(DGuildId))]
    public DGuild DGuild { get; set; }

    public List<Infraction> Infractions { get; set; } = new();
    public List<Reminder> Reminders { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();

    public int CompareTo(object? obj)
    {
        if (obj is DMember member)
            return member.DMemberId < DMemberId ? 
                1 : member.DMemberId > DMemberId ? -1 : 0;

        string typeName = obj is null ? "null" : obj.GetType().Name;
        throw new ArgumentException($"Instances of type {typeName} are not supported.\n" +
                                    $"Make sure your instance is of type {GetType().Name}");
    }

    public override bool Equals(object? obj) =>
        obj is DMember member && member.DMemberId == DMemberId && member.DGuildId == DGuildId;

    public override int GetHashCode() => DMemberId.GetHashCode();
}
