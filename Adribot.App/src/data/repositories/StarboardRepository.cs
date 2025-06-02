using System.Collections.Generic;
using System.Linq;
using Adribot.Entities.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Data.Repositories;

public sealed class StarboardRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{
    public IEnumerable<Starboard> GetAllStarboards()
    {
        using AdribotContext botContext = CreateDbContext();

        return botContext.Starboards.Include(s => s.MessageLinks);
    }

    public Starboard? GetStarboardConfiguration(int id)
    {
        using AdribotContext botContext = CreateDbContext();
        
        return botContext.Starboards.FirstOrDefault(s => s.StarboardId == id);
    }
}
