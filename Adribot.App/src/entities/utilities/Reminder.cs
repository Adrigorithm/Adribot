using System;
using Adribot.Data;
using Adribot.Entities.Discord;
using Discord;

namespace Adribot.Entities.Utilities;

public class Reminder : IDataStructure
{
    public int ReminderId { get; set; }

    public ulong? Channel { get; set; }
    public DateTimeOffset Date { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string Content { get; set; }

    public int DMemberId { get; set; }
    public DMember DMember { get; set; }

    public EmbedBuilder GenerateEmbedBuilder() =>
        new()
        {
            Author = new EmbedAuthorBuilder { Name = $"{DMember.Mention}" },
            Title = "",
            Description = $"A reminder set on `{Date:g}` to trigger on {EndDate:g}\n\nwith content `{Content}`"
        };
}
