using System.Collections.Generic;
using Adribot.Data;
using Adribot.Entities.Discord;
using Discord;

namespace Adribot.Entities.Utilities;

public class IcsCalendar : IDataStructure
{
    public int IcsCalendarId { get; set; }

    public ulong ChannelId { get; set; }
    public string Name { get; set; }

    public List<Event> Events { get; set; } = [];

    public int DMemberId { get; set; }
    public DMember DMember { get; set; }

    public EmbedBuilder GenerateEmbedBuilder() =>
        new()
        {
            Author = new EmbedAuthorBuilder { Name = "Adrigorithm" },
            Title = Name,
            Description = $"A cached calendar `{Name}` containing {Events.Count} events. You probably want to see an individual event instead."
        };
}
