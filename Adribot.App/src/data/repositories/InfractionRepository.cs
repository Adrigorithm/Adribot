using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.src.constants.enums;
using Adribot.src.entities.discord;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data.repositories;

public sealed class InfractionRepository : BaseRepository
{
    public InfractionRepository(IDbContextFactory<AdribotContext> _botContextFactory) : base(_botContextFactory) { }

    public IEnumerable<Infraction> GetInfractionsToOldNotExpired()
    {
        using AdribotContext _botContext = CreateDbContext();

        return _botContext.Infractions.Include(i => i.DMember).Include(i => i.DMember.DGuild).OrderByDescending(i => i.EndDate).Where(i => !i.IsExpired).ToList();
    }

    public Infraction AddInfraction(ulong guildId, ulong memberId, DateTimeOffset endDate, InfractionType type, string reason = "No reason provided", bool isExpired = false)
    {
        using AdribotContext _botContext = CreateDbContext();

        DateTimeOffset now = DateTimeOffset.UtcNow;
        var infraction = new Infraction
        {
            Date = now,
            DMember = _botContext.DMembers.Include(dm => dm.DGuild).First(dm => dm.MemberId == memberId && dm.DGuild.GuildId == guildId),
            EndDate = endDate,
            IsExpired = isExpired,
            Reason = reason,
            Type = type
        };

        _botContext.Add(infraction);
        _botContext.SaveChanges();

        return infraction;
    }

    public void SetExpiredStatus(Infraction infraction, bool isExpired)
    {
        using AdribotContext _botContext = CreateDbContext();

        _botContext.Infractions.First(i => i.InfractionId == infraction.InfractionId).IsExpired = isExpired;
        _botContext.SaveChanges();
    }
}
