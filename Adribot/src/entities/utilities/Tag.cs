using System;
using Adribot.src.data;
using Adribot.src.entities.discord;
using DSharpPlus.Entities;

namespace Adribot.src.entities.utilities;

public class Tag : IDataStructure
{
    public int TagId { get; }

    public string Name { get; set; }
    public string Content { get; set; }
    public DateTimeOffset Date { get; set; }

    public int DMemberId { get; set; }
    public DMember DMember { get; set; }

    public DiscordEmbedBuilder GenerateEmbedBuilder()
    {
        var tagContent = Content;
        if (Content.Length > 100)
            tagContent = Content.Substring(0, 100) + " ...";

        return new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = $"{DMember.Mention}" },
            Title = $"{Name}",
            Description = $"{tagContent}"
        };
    }

    public override bool Equals(object other) =>
        other is Tag tag && Name == tag.Name && DMember.DGuildId == tag.DMember.DGuildId;
}
