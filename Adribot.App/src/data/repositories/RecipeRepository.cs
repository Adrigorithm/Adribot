using System.Collections.Generic;
using System.Linq;
using Adribot.entities.fun.recipe;
using Microsoft.EntityFrameworkCore;

namespace Adribot.data.repositories;

public sealed class RecipeRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{
    public IEnumerable<Recipe> GetAllRecipes()
    {
        using AdribotContext botContext = CreateDbContext();

        return botContext.Recipes.Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).ToList();
    }
}
