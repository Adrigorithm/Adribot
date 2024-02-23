using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.src.config;
using Adribot.src.data;
using Adribot.src.entities.utilities;
using DSharpPlus.Entities;

namespace Adribot.src.entities.discord;

public class DMember : IComparable, IDataStructure
{
    public int DMemberId { get; }

    public ulong MemberId { get; set; }
    public string Mention { get; set; }
    
    public int DGuildId { get; set; }
    public DGuild DGuild { get; set; }

    public List<Infraction> Infractions { get; } = [];
    public List<Reminder> Reminders { get; } = [];
    public List<Tag> Tags { get; } = [];

    public int CompareTo(object? obj)
    {
        if (obj is DMember member)
        {
            return member.MemberId < MemberId ?
                1 : member.MemberId > MemberId ? -1 : 0;
        }

        var typeName = obj is null ? "null" : obj.GetType().Name;
        throw new ArgumentException($"Instances of type {typeName} are not supported.\n" +
                                    $"Make sure your instance is of type {GetType().Name}");
    }

    public override bool Equals(object? obj) =>
        obj is DMember member && member.MemberId == MemberId && member.DGuildId == DGuildId;
    public DiscordEmbedBuilder GenerateEmbedBuilder() =>
        new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = "<@608275633218519060>" },
            Color = new DiscordColor(Config.Configuration.EmbedColour),
            Title = MemberId.ToString(),
            Description = $"This member has set {Reminders.Count} reminders and {Tags.Count} tags.\n" +
                $"They have {Infractions.Count} infractions of which {Infractions.Count(i => !i.IsExpired)} are still pending."
        };

    public override int GetHashCode() => MemberId.GetHashCode();
}
