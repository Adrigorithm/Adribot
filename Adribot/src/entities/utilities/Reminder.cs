using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.entities.discord;

namespace Adribot.entities.utilities;

[Table("reminders")]
public class Reminder
{
    [Key]
    [Column("reminderid")]
    public int ReminderId { get; set; }

    [Column("date")]
    public DateTimeOffset Date { get; set; }

    [Column("enddate")]
    public DateTimeOffset EndDate { get; set; }

    [Column("content")]
    public string Content { get; set; }

    [ForeignKey("dmemberid")]
    public ulong DMemberId { get; set; }
    public DMember DMember { get; set; }
}
