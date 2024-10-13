using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.entities.utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.data.repositories;

public sealed class IcsCalendarRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{
    public IEnumerable<IcsCalendar> GetIcsCalendarsNotExpired()
    {
        using AdribotContext botContext = CreateDbContext();

        return botContext.IcsCalendars.Include(c => c.Events).Include(c => c.DMember).Where(c => c.Events.OrderBy(e => e.EventId).Last().End > DateTimeOffset.Now).ToList();
    }

    public void ChangeEventsPostedStatus(Dictionary<int, List<(int eventId, bool posted)>> events)
    {
        using AdribotContext botContext = CreateDbContext();

        foreach (var icsCalendarId in events.Keys)
        {
            IcsCalendar calendar = botContext.IcsCalendars.Include(c => c.Events).First(c => c.IcsCalendarId == icsCalendarId);
            events[icsCalendarId].ForEach(eCi => calendar.Events.First(e => e.EventId == eCi.eventId).IsPosted = eCi.posted);
        }

        botContext.SaveChanges();
    }

    public IcsCalendar AddCalendar(string calendarName, ulong guildId, ulong memberId, ulong channelId, IEnumerable<Event> events)
    {
        using AdribotContext botContext = CreateDbContext();

        var calendar = new IcsCalendar
        {
            ChannelId = channelId,
            Name = calendarName,
            DMember = botContext.DMembers.Include(dm => dm.DGuild).First(dm => dm.MemberId == memberId && dm.DGuild.GuildId == guildId),
            Events = events.ToList()
        };

        botContext.IcsCalendars.Add(calendar);
        botContext.SaveChanges();

        return calendar;
    }

    public void RemoveCalendar(IcsCalendar calendar)
    {
        using AdribotContext botContext = CreateDbContext();

        botContext.Remove(calendar);
        botContext.SaveChanges();
    }
}
