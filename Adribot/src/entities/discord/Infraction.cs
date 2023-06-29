using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.constants.enums;

namespace Adribot.entities.discord;

[Table("infractions")]
public class Infraction
{
    [Key]
    [Column("infractionid")]
    public int InfractionId { get; set; }

    [Column("date")]
    public DateTimeOffset Date { get; set; }

    [Column("enddate")]
    public DateTimeOffset EndDate { get; set; }

    [Column("type")]
    public InfractionType Type { get; set; }

    [Column("isexpired")]
    public bool IsExpired { get; set; }
    
    [ForeignKey("dmemberid")]
    public ulong DMemberId { get; set; }
    public DMember DMember { get; set; }
}
