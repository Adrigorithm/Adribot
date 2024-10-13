using System;
using Adribot.constants.enums;
using Adribot.data;
using Discord;

namespace Adribot.entities.discord;

public class Infraction : IDataStructure
{
    public int InfractionId { get; set; }

    public DateTimeOffset Date { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public InfractionType Type { get; set; }
    public bool IsExpired { get; set; }
    public string Reason { get; set; }

    public int DMemberId { get; set; }
    public DMember DMember { get; set; }

    public EmbedBuilder GenerateEmbedBuilder() =>
        new()
        {
            Author = new EmbedAuthorBuilder { Name = "<@608275633218519060>" },
            Title = $"{Type}",
            Description = $"This infraction belongs to {DMember.Mention}.\n" +
                $"It lastst from {Date:g} to {EndDate:g}\n" +
                $"It was issued because `{Reason}`"
        };
}
