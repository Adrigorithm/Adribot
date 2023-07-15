using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.constants.enums;
using Adribot.src.data;

namespace Adribot.entities.discord;

public class Infraction : IDataStructure
{
    [Key]
    public int InfractionId { get; set; }

    public DateTimeOffset Date { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public InfractionType Type { get; set; }
    public bool IsExpired { get; set; }
    public string Reason { get; set; }

    public int MemberId { get; set; }
    [ForeignKey(nameof(MemberId))]
    public DMember DMember { get; set; }
}
