using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.entities.discord;

namespace Adribot.entities.utilities;

[Table("tags")]
public class Tag
{
    [Key]
    [Column("tagid")]
    public int TagId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("content")]
    public string Content { get; set; }

    [Column("date")]
    public DateTimeOffset Date { get; set; }

    [ForeignKey("dmemberid")]
    public ulong DMemberId { get; set; }
    public DMember DMember { get; set; }
}
