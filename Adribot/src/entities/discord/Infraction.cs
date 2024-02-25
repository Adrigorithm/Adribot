using System;
using Adribot.src.constants.enums;
using Adribot.src.data;
using DSharpPlus.Entities;

namespace Adribot.src.entities.discord;

public class Infraction : IDataStructure
{
    public int InfractionId { get; }

    public DateTimeOffset Date { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public InfractionType Type { get; set; }
    public bool IsExpired { get; set; }
    public string Reason { get; set; }

    public int DMemberId { get; set; }
    public DMember DMember { get; set; }

    public DiscordEmbedBuilder GenerateEmbedBuilder() =>
        new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = "<@608275633218519060>" },
            Title = $"{Type}",
            Description = $"This infraction belongs to {DMember.Mention}.\n" +
                $"It lastst from {Date:g} to {EndDate:g}\n" +
                $"It was issued because `{Reason}`"
        };
}
