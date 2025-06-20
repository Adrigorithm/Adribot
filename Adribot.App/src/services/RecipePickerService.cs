using System.Collections.Generic;
using System.Linq;
using Adribot.Data.Repositories;
using Adribot.Entities.Fun.Recipe;
using Discord;

namespace Adribot.Services;

public sealed class RecipeService
{
    private readonly IEnumerable<Recipe> _recipes;

    public RecipeService(RecipeRepository recipeRepository) =>
        _recipes = recipeRepository.GetAllRecipes();

    public ComponentBuilderV2? GetRecipe(string recipeName)
    {
        Recipe recipe = _recipes.FirstOrDefault(r => r.Name.ToLower() == recipeName.ToLower());

        if (recipe is null)
            return null;

        ComponentBuilderV2 components = new ComponentBuilderV2()
            .WithContainer()
                .WithTextDisplay($"# {recipe.Name}")
                .WithMediaGallery(["https://cdn.discordapp.com/attachments/964253122547552349/1336440069892083712/7Q3S.gif?ex=67a3d04e&is=67a27ece&hm=059c9d28466f43a50c4b450ca26fc01298a2080356421d8524384bf67ea8f3ab&"])
                .WithActionRow([
                    new TextInputBuilder {

                    }
                ])
                .WithTextDisplay("""
                    ## Ingredients
                """)
                .WithTextDisplay("""
                    ## Instructions
                """);


        return components;
    }
