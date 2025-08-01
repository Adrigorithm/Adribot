using System.Collections.Generic;
using System.Linq;
using Adribot.Entities.fun;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Data.Repositories;

public sealed class WireRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{
    public List<WireConfig> GetAllWireConfigs()
    {
        using AdribotContext botContext = CreateDbContext();

        return botContext.WireConfigs.Include(w => w.DGuild).ToList();
    }

    public void AddWireConfig(WireConfig wireConfig)
    {
        using AdribotContext botContext = CreateDbContext();

        botContext.WireConfigs.Add(wireConfig);
        botContext.SaveChanges();
    }
}
