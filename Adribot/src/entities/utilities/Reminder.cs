using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.src.config;
using Adribot.src.data;
using Adribot.src.entities.discord;
using DSharpPlus.Entities;

namespace Adribot.src.entities.utilities;

public class Reminder : IDataStructure
{
    [Key]
    public int ReminderId { get; set; }

    public ulong? Channel { get; set; }
    public DateTimeOffset Date { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string Content { get; set; }

    public ulong DGuildId { get; set; }
    public ulong DMemberId { get; set; }
    [ForeignKey($"{nameof(DGuildId)}, {nameof(DMemberId)}")]
    public virtual DMember DMember { get; set; }

    public DiscordEmbedBuilder GenerateEmbedBuilder() =>
        new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = $"<@{DMemberId}>" },
            Color = new DiscordColor(Config.Configuration.EmbedColour),
            Title = "",
            Description = $"A reminder set on `{Date:g}` to trigger on {EndDate:g}\n\nwith content `{Content}`"
        };
}
