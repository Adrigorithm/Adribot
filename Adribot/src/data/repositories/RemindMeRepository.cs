using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.src.entities.utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data.repositories;

public class RemindMeRepository(AdribotContext _botContext)
{
    public IEnumerable<Reminder> GetRemindersToOld() =>
        _botContext.Reminders.Include(r => r.DMember).Include(r => r.DMember.DGuild).OrderByDescending(r => r.EndDate);

    public void RemoveReminder(Reminder reminder)
    {
        _botContext.Remove(reminder);
        _botContext.SaveChanges();
    }

    public Reminder AddRemindMe(ulong guildId, ulong memberId, ulong channelId, string content, DateTimeOffset endDate)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        var reminder = new Reminder
        {
            Channel = channelId,
            Content = content,
            Date = now,
            EndDate = endDate,
            DMember = _botContext.DMembers.Include(dm => dm.DGuild).First(dm => dm.MemberId == memberId && dm.DGuild.GuildId == guildId)
        };

        _botContext.Add(reminder);
        _botContext.SaveChanges();

        return reminder;
    }

}
