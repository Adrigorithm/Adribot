using System.Collections.Generic;
using Adribot.src.data;
using Adribot.src.entities.discord;
using DSharpPlus.Entities;

namespace Adribot.src.entities.utilities;

public class IcsCalendar : IDataStructure
{
    public int IcsCalendarId { get; set; }

    public ulong ChannelId { get; set; }
    public string Name { get; set; }

    public List<Event> Events { get; set; } = [];

    public int DMemberId { get; set; }
    public DMember DMember { get; set; }

    public DiscordEmbedBuilder GenerateEmbedBuilder() =>
        new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = "Adrigorithm" },
            Title = Name,
            Description = $"A cached calendar `{Name}` containing {Events.Count} events. You probably want to see an individual event instead."
        };
}
