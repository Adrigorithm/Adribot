using System;
using System.ComponentModel.DataAnnotations.Schema;
using Adribot.src.config;
using Adribot.src.data;
using DSharpPlus.Entities;

namespace Adribot.src.entities.utilities
{
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
        public virtual IcsCalendar IcsCalendar { get; set; }

        public DiscordEmbedBuilder GenerateEmbedBuilder() =>
        new DiscordEmbedBuilder
        {
            Author = new DiscordEmbedBuilder.EmbedAuthor() { Name = Organiser },
            Color = new DiscordColor(Config.Configuration.EmbedColour),
            Title = $"{Name}\n[{Start:HHmm} - {End:HHmm}]",
            Description = $"{Summary}",
            Footer = new DiscordEmbedBuilder.EmbedFooter() { Text = Location }
        };
    }
}
