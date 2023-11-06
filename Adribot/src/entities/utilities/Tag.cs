using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.src.config;
using Adribot.src.data;
using Adribot.src.entities.discord;
using DSharpPlus.Entities;

namespace Adribot.src.entities.utilities;

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

    public DiscordEmbedBuilder GenerateEmbedBuilder()
    {
        var tagContent = Content;
        if (Content.Length > 100)
            tagContent = Content.Substring(0, 100) + " ...";

        return new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = $"<@{DMemberId}>" },
            Color = new DiscordColor(Config.Configuration.EmbedColour),
            Title = $"{Name}",
            Description = $"{tagContent}"
        };
    }

}
