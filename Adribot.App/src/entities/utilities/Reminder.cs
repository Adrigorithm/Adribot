using System;
using Adribot.src.data;
using Adribot.src.entities.discord;
using DSharpPlus.Entities;

namespace Adribot.src.entities.utilities;

public class Reminder : IDataStructure
{
    public int ReminderId { get; set; }

    public ulong? Channel { get; set; }
    public DateTimeOffset Date { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string Content { get; set; }

    public int DMemberId { get; set; }
    public DMember DMember { get; set; }

    public DiscordEmbedBuilder GenerateEmbedBuilder() =>
        new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = $"{DMember.Mention}" },
            Title = "",
            Description = $"A reminder set on `{Date:g}` to trigger on {EndDate:g}\n\nwith content `{Content}`"
        };
}
