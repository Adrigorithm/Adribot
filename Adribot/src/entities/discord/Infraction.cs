using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.src.config;
using Adribot.src.constants.enums;
using Adribot.src.data;
using DSharpPlus.Entities;

namespace Adribot.src.entities.discord;

public class Infraction : IDataStructure
{
    [Key]
    public int? InfractionId { get; set; }

    public DateTimeOffset Date { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public InfractionType Type { get; set; }
    public bool IsExpired { get; set; }
    public string Reason { get; set; }

    public ulong DGuildId { get; set; }
    public ulong DMemberId { get; set; }
    [ForeignKey($"{nameof(DGuildId)}, {nameof(DMemberId)}")]
    public virtual DMember DMember { get; set; }

    public DiscordEmbedBuilder GenerateEmbedBuilder() =>
        new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = "<@608275633218519060>" },
            Color = new DiscordColor(Config.Configuration.EmbedColour),
            Title = $"{Type}",
            Description = $"This infraction belongs to {DMember.Mention}.\n" +
                $"It lastst from {Date:g} to {EndDate:g}\n" +
                $"It was issued because `{Reason}`"
        };
}
