using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Adribot.Data.Repositories;
using Adribot.Entities.Fun.Recipe;
using Adribot.Helpers;
using Discord;

namespace Adribot.Services;

public sealed class RecipeService
{
    private IEnumerable<Recipe> _recipes;

    public RecipeService(RecipeRepository recipeRepository) =>
        _recipes = recipeRepository.GetAllRecipes();

    public ComponentBuilderV2? GetRecipe(string recipeName)
    {
        var recipe = _recipes.FirstOrDefault(r => r.Name.ToLower() == recipeName.ToLower());

        if (recipe is null)
            return null;

        var components = new ComponentBuilderV2()
            .WithContainer()
                .WithTextDisplay($"# {recipe.Name}");
    }
