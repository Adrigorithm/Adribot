using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Data.Repositories;
using Adribot.Entities.Fun.Recipe;
using Adribot.Extensions;
using Adribot.Services.Providers;
using Discord;
using Discord.WebSocket;

namespace Adribot.Services;

public sealed class RecipeService
{
    private const int RecipeNameDisplay = 1;
    private const string RecipeUnitInput = "recipe-select-menu-unit";
    private const string RecipeServingsInput = "recipe-text-input-servings";
    private const int MinimumServings = 1;
    private const int MaximumServings = 999;
    
    private readonly IEnumerable<Recipe> _recipes;

    public RecipeService(DiscordClientProvider clientProvider, RecipeRepository recipeRepository)
    {
        _recipes = recipeRepository.GetAllRecipes();
        clientProvider.Client.InteractionCreated += ClientOnInteractionCreatedAsync;
    }

    private async Task ClientOnInteractionCreatedAsync(SocketInteraction arg)
    {
        if (arg is not SocketMessageComponent component)
            return;

        switch (component.Data.CustomId)
        {
            case RecipeServingsInput:
                if (!int.TryParse(component.Data.Value, out var value) || value < MinimumServings || value > MaximumServings)
                    return;

                var recipeName1 = component.Message.Components.FindComponentById<TextDisplayComponent>(RecipeNameDisplay).Content;
                Recipe recipe1 = _recipes.First(r => r.Name == recipeName1);

                recipe1.ToHumanReadable();
                await component.Message.ModifyAsync(m => m.Components = BuildComponentUnsafe(recipe1).Build());
                
                break;
            case RecipeUnitInput:
                var unitValue = int.Parse(component.Data.Value);
                var recipeName2 = component.Message.Components.FindComponentById<TextDisplayComponent>(RecipeNameDisplay).Content;
                Recipe recipe2 = _recipes.First(r => r.Name == recipeName2).ConvertNumerals((Units)Enum.ToObject(typeof(Units), unitValue));
                
                recipe2.ToHumanReadable();
                await component.Message.ModifyAsync(m => m.Components = BuildComponentUnsafe(recipe2).Build());
                
                break;
            default:
                return;
        }
    }

    private static ComponentBuilderV2 BuildComponentUnsafe(Recipe recipe)
    {
        StringBuilder ingredients = new($"## Ingredients{Environment.NewLine}");
        StringBuilder instructions = new($"## Instructions{Environment.NewLine}");

        foreach (RecipeIngredient recipeIngredient in recipe.RecipeIngredients)
        {
            ingredients.AppendLine($"{recipeIngredient.Quantity}{recipeIngredient.Unit.ToSymbol()} {recipeIngredient.Ingredient.Name} ");

            if (recipeIngredient.Optional)
                ingredients.Append("[Optional]");
        }
        
        foreach (var instruction in recipe.Instruction)
            instructions.AppendLine($"{instruction}{Environment.NewLine}");
        
        return new ComponentBuilderV2()
            .WithContainer()
            .WithTextDisplay($"# {recipe.Name}", RecipeNameDisplay)
            .WithMediaGallery([
                "https://cdn.discordapp.com/attachments/964253122547552349/1336440069892083712/7Q3S.gif?ex=67a3d04e&is=67a27ece&hm=059c9d28466f43a50c4b450ca26fc01298a2080356421d8524384bf67ea8f3ab&"
            ])
            .WithActionRow(
            [
                new TextInputBuilder()
                    .WithCustomId(RecipeServingsInput)
                    .WithLabel("Servings")
                    .WithValue(recipe.Servings.ToString())
                    .WithMinLength(1)
                    .WithMaxLength(3)
                    .WithStyle(TextInputStyle.Short)
            ])
            .WithTextDisplay(ingredients.ToString())
            .WithActionRow([
                new SelectMenuBuilder(
                    RecipeUnitInput,
                    [
                        new SelectMenuOptionBuilder(
                            "Metric",
                            "1",
                            isDefault: true),
                        new SelectMenuOptionBuilder(
                            "Imperial",
                            "2"),
                        new SelectMenuOptionBuilder(
                            "SI",
                            "0")
                    ],
                    id: 1
                )
            ])
            .WithTextDisplay(instructions.ToString());
    }

    private static ComponentBuilderV2? BuildComponent(Recipe? recipe) => 
        recipe is null ? null : BuildComponentUnsafe(recipe);

    public ComponentBuilderV2? GetRecipeComponent(int recipeId) =>
        BuildComponent(_recipes.FirstOrDefault(r => r.RecipeId == recipeId));
    
    private ComponentBuilderV2? GetRecipeComponent(string recipeName) =>
        BuildComponent(_recipes.FirstOrDefault(r => r.Name == recipeName));
}
