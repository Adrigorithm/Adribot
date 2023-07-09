using System;
using Adribot.entities.discord;
using Adribot.src.data;

namespace Adribot.entities.utilities;

public class Tag : IDataStructure
{
    public int TagId { get; set; }

    public string Name { get; set; }
    public string Content { get; set; }
    public DateTimeOffset Date { get; set; }

    public DMember DMember { get; set; }
}
