using Adribot.src.config;
using Adribot.src.data;
using Adribot.src.entities.utilities;
using DSharpPlus.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Adribot.src.entities.discord;

[PrimaryKey(nameof(DGuildId), nameof(DMemberId))]
public class DMember : IComparable, IDataStructure
{
    public ulong DGuildId { get; set; }
    public ulong DMemberId { get; set; }
    [ForeignKey(nameof(DGuildId))]
    public virtual DGuild DGuild { get; set; }

    public virtual List<Infraction> Infractions { get; set; } = new();
    public virtual List<Reminder> Reminders { get; set; } = new();
    public virtual List<Tag> Tags { get; set; } = new();

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
    public DiscordEmbedBuilder GenerateEmbedBuilder() =>
        new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = "<@608275633218519060>" },
            Color = new DiscordColor(Config.Configuration.EmbedColour),
            Title = DMemberId.ToString(),
            Description = $"This member has set {Reminders.Count} reminders and {Tags.Count} tags.\n" +
                $"They have {Infractions.Count} infractions of which {Infractions.Count(i => !i.IsExpired)} are still pending."
        };

    public override int GetHashCode() => DMemberId.GetHashCode();
}
