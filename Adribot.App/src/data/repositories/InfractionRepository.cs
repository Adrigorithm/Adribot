using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.Constants.Enums;
using Adribot.Entities.Discord;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Data.Repositories;

public sealed class InfractionRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{
    public IEnumerable<Infraction> GetInfractionsToOldNotExpired()
    {
        using AdribotContext botContext = CreateDbContext();

        DbSet<Infraction> infractions = botContext.Infractions;

        return infractions.Include(i => i.DMember).ThenInclude(m => m.DGuild).OrderByDescending(i => i.EndDate).Where(i => !i.IsExpired).ToList();
    }

    public Infraction AddInfraction(ulong guildId, ulong memberId, DateTimeOffset endDate, InfractionType type, string reason = "No reason provided", bool isExpired = false)
    {
        using AdribotContext botContext = CreateDbContext();

        DateTimeOffset now = DateTimeOffset.UtcNow;
        var infraction = new Infraction
        {
            Date = now,
            DMember = botContext.DMembers.Include(dm => dm.DGuild).First(dm => dm.MemberId == memberId && dm.DGuild.GuildId == guildId),
            EndDate = endDate,
            IsExpired = isExpired,
            Reason = reason,
            Type = type
        };

        botContext.Add(infraction);
        botContext.SaveChanges();

        return infraction;
    }

    public void SetExpiredStatus(Infraction infraction, bool isExpired)
    {
        using AdribotContext botContext = CreateDbContext();

        botContext.Infractions.First(i => i.InfractionId == infraction.InfractionId).IsExpired = isExpired;
        botContext.SaveChanges();
    }
}
