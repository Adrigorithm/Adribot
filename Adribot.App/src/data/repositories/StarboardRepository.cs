using Adribot.Entities.Discord;
using Adribot.Entities.Utilities;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Adribot.Data.Repositories;

public sealed class StarboardRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{
    public IEnumerable<Starboard> GetStarboards()
    {
        using AdribotContext botContext = CreateDbContext();

        return botContext.Starboards.Include(s => s.DGuild);
    }

    public IEnumerable<DMessage> GetStarredMessages() {
        using AdribotContext botContext = CreateDbContext();

        return botContext.DMessages.Include(m => m.Starboard).Include(m => m.Starboard.DGuild).Include(m => m.DMember);
    }

    public void SetupStarBoard(Starboard starboard) {
        using AdribotContext botContext = CreateDbContext();

        botContext.Add(starboard);
        botContext.SaveChanges();
    }
}
