using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Adribot.src.data;
using Adribot.src.entities.utilities;

namespace Adribot.entities.discord;

public class IcsCalendar : IDataStructure
{
    [Key]
    public int IcsCalendarId { get; set; }
    public ulong ChannelId { get; set; }

    public virtual List<Event> Events { get; set; } = new();

    public ulong DGuildId { get; set; }
    public virtual DGuild DGuild { get; set; }
}
