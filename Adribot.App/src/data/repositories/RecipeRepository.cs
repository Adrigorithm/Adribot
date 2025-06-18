using System.Collections.Generic;
using System.Linq;
using Adribot.Entities.Fun.Recipe;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Data.Repositories;

public sealed class RecipeRepository(IDbContextFactory<AdribotContext> botContextFactory)
: BaseRepository(botContextFactory)
{
    public IEnumerable<Recipe> GetAllRecipes()
    {
        using AdribotContext botContext = CreateDbContext();

        return botContext.Recipes.Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).ToList();
    }
}
