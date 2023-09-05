using Adribot.src.entities.utilities;
using Ical.Net.CalendarComponents;

namespace Adribot.src.extensions
{
    public static class ICallNetConversions
    {
        public static Event ToEvent(this CalendarEvent cEvent) =>
            new Event()
            {
                End = cEvent.End.AsDateTimeOffset,
                isAllDay = cEvent.IsAllDay,
                Location = cEvent.Location,
                Start = cEvent.Start.AsDateTimeOffset,
                Description = cEvent.Description,
                Name = cEvent.Name,
                Organiser = cEvent.Organizer.CommonName,
                Summary = cEvent.Summary,
            };
    }
}
