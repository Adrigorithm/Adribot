using System;
using Adribot.entities.discord;
using Adribot.src.data;

namespace Adribot.entities.utilities;

public class Reminder : IDataStructure
{
    public int ReminderId { get; set; }

    public DateTimeOffset Date { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string Content { get; set; }

    public DMember DMember { get; set; }
}
