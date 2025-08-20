using System;
using Adribot.Entities.Utilities;
using Ical.Net.CalendarComponents;

namespace Adribot.Extensions;

public static class CallNetConversions
{
    public static Event ToEvent(this CalendarEvent cEvent) =>
        new()
        {
            End = new DateTimeOffset(cEvent.End?.AsUtc ?? DateTime.MaxValue, TimeSpan.Zero),
            IsAllDay = cEvent.IsAllDay,
            Location = string.IsNullOrEmpty(cEvent.Location) ? "Unknown" : cEvent.Location,
            Start = new DateTimeOffset(cEvent.Start?.AsUtc ?? DateTime.MinValue, TimeSpan.Zero),
            Description = string.IsNullOrEmpty(cEvent.Description) ? "No description found" : cEvent.Description,
            Name = string.IsNullOrEmpty(cEvent.Name) ? "Unnamed" : cEvent.Name,
            Organiser = cEvent.Organizer?.CommonName ?? "Unknown",
            Summary = string.IsNullOrEmpty(cEvent.Summary) ? "No summary found" : cEvent.Summary
        };
}
