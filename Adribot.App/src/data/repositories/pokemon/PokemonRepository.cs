using System.Collections.Generic;
using System.Linq;
using Adribot.entities.fun.pokemon;
using Microsoft.EntityFrameworkCore;

namespace Adribot.data.repositories.pokemon;

public sealed class PokemonRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{
    public T Add<T>(T pokemonObject)
    {
        using AdribotContext dbContext = CreateDbContext();
        
        dbContext.Add(pokemonObject);
        dbContext.SaveChanges();
        
        return pokemonObject;
    }

    public List<T> AddRange<T>(List<T> pokemonObjects)
    {
        using AdribotContext dbContext = CreateDbContext();
        
        dbContext.AddRange(pokemonObjects);
        dbContext.SaveChanges();
        
        return pokemonObjects;
    }

    public List<T> GetAll<T>() where T : class
    {
        using AdribotContext dbContext = CreateDbContext();

        return dbContext.Set<T>().ToList();
    }
}
