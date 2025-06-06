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

    public Starboard? GetStarboardConfiguration(ulong guildId)
    {
        using AdribotContext botContext = CreateDbContext();
        
        return botContext.Starboards.Include(s => s.DGuild).Include(s => s.MessageLinks).FirstOrDefault(s => s.DGuild.GuildId == guildId);
    }

    public void SetStarboardConfiguration(ulong guildId, Starboard starboard, bool exists)
    {
        using AdribotContext botContext = CreateDbContext();
        
        if (exists)
            botContext.Starboards.Update(starboard);
        else
            botContext.Starboards.Add(starboard);
        
        botContext.SaveChanges();
    }

    public void RemoveMessageLink(MessageLink messageLink)
    {
        using AdribotContext botContext = CreateDbContext();
        
        botContext.MessageLinks.Remove(messageLink);
        botContext.SaveChanges();
    }

    public void AddMessageLink(MessageLink messageLink)
    {
        using AdribotContext botContext = CreateDbContext();
        
        botContext.MessageLinks.Add(messageLink);
        botContext.SaveChanges();
    }
}
