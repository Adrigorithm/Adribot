using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.entities.discord;
using Adribot.src.data;

namespace Adribot.entities.utilities;

public class Reminder : IDataStructure
{
    [Key]
    public int ReminderId { get; set; }

    public DateTimeOffset Date { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string Content { get; set; }

    public ulong DMemberId { get; set; }
    public ulong DGuildId { get; set; }
    [ForeignKey($"{nameof(DMemberId)}, {nameof(DGuildId)}")]
    public DMember DMember { get; set; }
}
