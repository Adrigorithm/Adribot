using System;
using Adribot.data;
using Adribot.entities.discord;
using Discord;

namespace Adribot.entities.utilities;

public class Tag : IDataStructure
{
    public int TagId { get; set; }

    public string Name { get; set; }
    public string Content { get; set; }
    public DateTimeOffset Date { get; set; }

    public int DMemberId { get; set; }
    public DMember DMember { get; set; }

    public EmbedBuilder GenerateEmbedBuilder()
    {
        var tagContent = Content;
        if (Content.Length > 100)
            tagContent = string.Concat(Content.AsSpan(0, 100), " ...");

        return new EmbedBuilder
        {
            Author = new EmbedAuthorBuilder
            {
                Name = $"{DMember.Mention}"
            },
            Title = $"{Name}",
            Description = $"{tagContent}"
        };
    }

    public void Overwrite(Tag tag)
    {
        Content = tag.Content;
        Date = DateTimeOffset.UtcNow;
    }

    public override bool Equals(object other) =>
        other is Tag tag && Name == tag.Name && DMember.DGuildId == tag.DMember.DGuildId;

    public override int GetHashCode() =>
        base.GetHashCode();
}
