using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.Entities.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Data.Repositories;

public sealed class RemindMeRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{
    public IEnumerable<Reminder> GetRemindersToOld()
    {
        using AdribotContext botContext = CreateDbContext();

        return botContext.Reminders.Include(r => r.DMember).Include(r => r.DMember.DGuild).OrderByDescending(r => r.EndDate).ToList();
    }

    public void RemoveReminder(Reminder reminder)
    {
        using AdribotContext botContext = CreateDbContext();

        botContext.Remove(reminder);
        botContext.SaveChanges();
    }

    public Reminder AddRemindMe(ulong guildId, ulong memberId, ulong? channelId, string content, DateTimeOffset endDate)
    {
        using AdribotContext botContext = CreateDbContext();

        DateTimeOffset now = DateTimeOffset.UtcNow;
        var reminder = new Reminder
        {
            Channel = channelId,
            Content = content,
            Date = now,
            EndDate = endDate,
            DMember = botContext.DMembers.Include(dm => dm.DGuild).First(dm => dm.MemberId == memberId && dm.DGuild.GuildId == guildId)
        };

        botContext.Add(reminder);
        botContext.SaveChanges();

        return reminder;
    }
}
