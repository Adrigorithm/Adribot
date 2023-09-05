using Adribot.entities.discord;
using Adribot.src.data;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adribot.src.entities.utilities
{
    public class Event : IDataStructure
    {
        public int EventId { get; set; }

        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public bool isAllDay { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Organiser { get; set; }
        public string Summary { get; set; }

        [NotMapped]
        public bool isPosted {get; set;}
        [NotMapped]
        public TimeSpan Duration =>
            Start - End;

        public int IcsCalendarId { get; set; }
        public virtual IcsCalendar IcsCalendar { get; set; }
    }
}
