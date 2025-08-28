using System.Collections.Generic;
using Adribot.entities.fun.pokemon;
using Microsoft.EntityFrameworkCore;

namespace Adribot.data.repositories.pokemon;

public sealed class PokemonRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{
    public void Add(object pokemonObject)
    {
        using AdribotContext dbContext = CreateDbContext();
        
        dbContext.Add(pokemonObject);
        dbContext.SaveChanges();
    }

    public void AddRange(IEnumerable<object> pokemonObjects)
    {
        using AdribotContext dbContext = CreateDbContext();
        
        dbContext.AddRange(pokemonObjects);
        dbContext.SaveChanges();
    }
}
