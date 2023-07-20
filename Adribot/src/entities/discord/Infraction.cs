using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.constants.enums;
using Adribot.src.data;

namespace Adribot.entities.discord;

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
}
