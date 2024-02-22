using Adribot.src.entities.utilities;
using Ical.Net.CalendarComponents;

namespace Adribot.src.extensions;

public static class CallNetConversions
{
    public static Event ToEvent(this CalendarEvent cEvent) =>
        new Event()
        {
            End = cEvent.End.AsDateTimeOffset,
            IsAllDay = cEvent.IsAllDay,
            Location = string.IsNullOrEmpty(cEvent.Location) ? "Unknown" : cEvent.Location,
            Start = cEvent.Start.AsDateTimeOffset,
            Description = string.IsNullOrEmpty(cEvent.Description) ? "No description found" : cEvent.Description,
            Name = string.IsNullOrEmpty(cEvent.Name) ? "Unnamed" : cEvent.Name,
            Organiser = cEvent.Organizer?.CommonName is null ? "Unknown" : cEvent.Organizer.CommonName,
            Summary = string.IsNullOrEmpty(cEvent.Summary) ? "No summary found" : cEvent.Summary,
        };
}