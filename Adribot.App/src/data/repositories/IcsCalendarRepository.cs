using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.src.entities.utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data.repositories;

public sealed class IcsCalendarRepository : BaseRepository
{
    public IcsCalendarRepository(IDbContextFactory<AdribotContext> _botContextFactory) : base(_botContextFactory) { }

    public IEnumerable<IcsCalendar> GetIcsCalendarsNotExpired()
    {
        using AdribotContext _botContext = CreateDbContext();

        return _botContext.IcsCalendars.Include(c => c.Events).Include(c => c.DMember).Where(c => c.Events.OrderBy(e => e.EventId).Last().End > DateTimeOffset.Now).ToList();
    }

    public void ChangeEventsPostedStatus(Dictionary<int, List<(int eventId, bool posted)>> events)
    {
        using AdribotContext _botContext = CreateDbContext();

        foreach (var icsCalendarId in events.Keys)
        {
            IcsCalendar calendar = _botContext.IcsCalendars.Include(c => c.Events).First(c => c.IcsCalendarId == icsCalendarId);
            events[icsCalendarId].ForEach(eCi => calendar.Events.First(e => e.EventId == eCi.eventId).IsPosted = eCi.posted);
        }

        _botContext.SaveChanges();
    }

    public IcsCalendar AddCalendar(string calendarName, ulong guildId, ulong memberId, ulong channelId, IEnumerable<Event> events)
    {
        using AdribotContext _botContext = CreateDbContext();

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
        using AdribotContext _botContext = CreateDbContext();

        _botContext.Remove(calendar);
        _botContext.SaveChanges();
    }
}
