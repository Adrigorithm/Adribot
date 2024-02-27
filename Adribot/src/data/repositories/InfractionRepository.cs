using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.src.constants.enums;
using Adribot.src.entities.discord;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data.repositories;

public class InfractionRepository(AdribotContext _botContext)
{
    public IEnumerable<Infraction> GetInfractionsToOldNotExpired() =>
        _botContext.Infractions.Include(i => i.DMember).Include(i => i.DMember.DGuild).OrderByDescending(i => i.EndDate).Where(i => !i.IsExpired);

    public Infraction AddInfraction(ulong guildId, ulong memberId, DateTimeOffset endDate, InfractionType type, string reason = "No reason provided", bool isExpired = false)
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        var infraction = new Infraction {
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
        _botContext.Infractions.First(i => i.InfractionId == infraction.InfractionId).IsExpired = isExpired;
        _botContext.SaveChanges();
    }
}