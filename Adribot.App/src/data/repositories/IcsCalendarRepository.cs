using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.src.entities.utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data.repositories;

public class IcsCalendarRepository(AdribotContext _botContext)
{
    public IEnumerable<IcsCalendar> GetIcsCalendarsNotExpired() =>
        _botContext.IcsCalendars.Include(c => c.Events).Include(c => c.DMember).Where(c => c.Events.Last().End > DateTimeOffset.Now);

    public void ChangeEventsPostedStatus(Dictionary<int, List<(int eventId, bool posted)>> events)
    {
        foreach (var icsCalendarId in events.Keys)
        {
            IcsCalendar calendar = _botContext.IcsCalendars.Include(c => c.Events).First(c => c.IcsCalendarId == icsCalendarId);
            events[icsCalendarId].ForEach(eCi => calendar.Events.First(e => e.EventId == eCi.eventId).IsPosted = eCi.posted);
        }

        _botContext.SaveChanges();
    }

    public IcsCalendar AddCalendar(string calendarName, ulong guildId, ulong memberId, ulong channelId, IEnumerable<Event> events)
    {
        var calendar = new IcsCalendar
        {
            ChannelId = channelId,
            Name = calendarName,
            DMember = _botContext.DMembers.Include(dm => dm.DGuild).First(dm => dm.MemberId == memberId && dm.DGuild.GuildId == guildId),
            Events = events.ToList()
        };

        _botContext.IcsCalendars.Add(calendar);
        _botContext.SaveChanges();

        return calendar;
    }

    public void RemoveCalendar(IcsCalendar calendar)
    {
        _botContext.Remove(calendar);
        _botContext.SaveChanges();
    }
}
