using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Adribot.entities.discord;
using Adribot.src.data;

namespace Adribot.entities.utilities;

public class Tag : IDataStructure
{
    [Key]
    public int TagId { get; set; }

    public string Name { get; set; }
    public string Content { get; set; }
    public DateTimeOffset Date { get; set; }

    public ulong DGuildId { get; set; }
    public ulong DMemberId { get; set; }
    [ForeignKey($"{nameof(DGuildId)}, {nameof(DMemberId)}")]
    public virtual DMember DMember { get; set; }

    public override string ToString()
    {
        var tagString = new StringBuilder($"-- Tag[{(TagId == 0 ? "Unknown" : TagId)} - {Name}] from Guild {DGuildId} --{Environment.NewLine}");
        tagString.Append($"Content: {Content[..(Content.Length < 100 ? Content.Length : 100)]}");
        if (Content.Length > 100)
            tagString.Append(" ...");

        return tagString.ToString();
    }
}
