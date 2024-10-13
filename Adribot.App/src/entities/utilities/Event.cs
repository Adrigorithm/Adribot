using System;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.data;
using Discord;

namespace Adribot.entities.utilities;

public class Event : IDataStructure
{
    public int EventId { get; set; }

    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public bool IsAllDay { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public string Organiser { get; set; }
    public string Summary { get; set; }

    [NotMapped]
    public bool IsPosted { get; set; }
    [NotMapped]
    public TimeSpan Duration =>
        Start - End;

    public int IcsCalendarId { get; set; }
    public IcsCalendar IcsCalendar { get; set; }

    public EmbedBuilder GenerateEmbedBuilder() =>
        new()
        {
            Author = new EmbedAuthorBuilder { Name = Organiser },
            Title = $"{Name}\n[{Start:HH:mm} - {End:HH:mm}]",
            Description = Summary,
            Footer = new EmbedFooterBuilder { Text = Location }
        };

    // ReSharper disable once InconsistentNaming
    public EmbedBuilder GeneratePXLEmbedBuilder()
    {
        var descriptionLines = Description.Split('\n');

        return new EmbedBuilder
        {
            Author = new EmbedAuthorBuilder { Name = descriptionLines[3].Substring(descriptionLines[3].IndexOf(':') + 2) },
            Title = $"{descriptionLines[5].Substring(descriptionLines[5].IndexOf(':') + 2)}\n{Location} - [{Start:HH:mm} -> {End:HH:mm}]",
            Description = Summary
        };
    }
}
