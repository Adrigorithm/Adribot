using Adribot.src.config;
using Adribot.src.data;
using Adribot.src.entities.discord;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Adribot.src.entities.utilities;

public class IcsCalendar : IDataStructure
{
    [Key]
    public int IcsCalendarId { get; set; }
    public ulong ChannelId { get; set; }
    public string Name { get; set; }

    public virtual List<Event> Events { get; set; } = new();

    public ulong DGuildId { get; set; }
    public virtual DGuild DGuild { get; set; }

    public DiscordEmbedBuilder GenerateEmbedBuilder() =>
        new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = "<@608275633218519060>" },
            Color = new DiscordColor(Config.Configuration.EmbedColour),
            Title = Name,
            Description = $"A cached calendar `{Name}` containing {Events.Count} events. You probably want to see an individual event instead."
        };
}
