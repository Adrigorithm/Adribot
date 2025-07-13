using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Data.Repositories;
using Adribot.Entities.Fun.Recipe;
using Adribot.Extensions;
using Adribot.Services.Providers;
using Discord;
using Discord.Interactions.Builders;
using Discord.WebSocket;
using ModalBuilder = Discord.ModalBuilder;

namespace Adribot.Services;

public sealed class RecipeService
{
    private const int RecipeNameDisplay = 1;
    private const int RecipeServingsDisplay = 2;
    private const int RecipeUnitSelectMenu = 3;
    private const string RecipeUnitInput = "recipe-select-menu-unit";
    private const string RecipeServingsInput = "recipe-text-input-servings";
    private const string RecipeServingsButton = "recipe-button-servings";
    private const string RecipeServingsModal = "recipe-modal-servings";
    
    private const string RecipesLookInsideButton = "recipes-show-me-button";

    private readonly DiscordClientProvider _clientProvider;
    private readonly IEnumerable<Recipe> _recipes;

    public RecipeService(DiscordClientProvider clientProvider, RecipeRepository recipeRepository)
    {
        _recipes = recipeRepository.GetAllRecipes();
        _clientProvider = clientProvider;
        
        _clientProvider.Client.InteractionCreated += ClientOnInteractionCreatedAsync;
    }

    private async Task ClientOnInteractionCreatedAsync(SocketInteraction arg)
    {
        switch (arg)
        {
            case SocketMessageComponent component:
                switch (component.Data.CustomId)
                {
                    case RecipeServingsModal:
                        var servings = short.Parse(component.Message.Components.FindComponentById<TextDisplayComponent>(RecipeServingsDisplay).Content.Split(' ')[1]);
                        
                        await component.RespondWithModalAsync(CreateServingsModal(servings).Build());

                        break;

                    case RecipeUnitInput:
                        SelectMenuComponent selectedItem = component.Message.Components.FindComponentById<SelectMenuComponent>(RecipeUnitSelectMenu);
                        var unitValue = short.Parse(component.Data.Values.First());
                        var recipeName = component.Message.Components.FindComponentById<TextDisplayComponent>(RecipeNameDisplay).Content[2..];
                        Recipe recipe = _recipes.First(r => r.Name == recipeName);
                        Recipe recipe0 = recipe.Clone();
                        var unit = (Units)Enum.ToObject(typeof(Units), unitValue);

                        ComponentBuilderV2 newComponentContainer = BuildComponentUnsafe(recipe0, unit);
                    
                        await component.UpdateAsync(m => m.Components = newComponentContainer.Build());

                        break;
                    default:
                        return;
                }
                
                break;
            case SocketModal modal:
                if (modal.Data.CustomId == RecipeServingsButton)
                {
                    var success = short.TryParse(modal.Data.Components.First(c => c.CustomId == RecipeServingsInput).Value, out var servings);
                    
                    if (!success || servings <= 0)
                        break;
                    
                    Recipe recipe = _recipes.First(r => r.Name == modal.Message.Components.FindComponentById<TextDisplayComponent>(RecipeNameDisplay).Content[2..]);
                    Recipe? recipe0 = recipe.Clone();
                    
                    recipe0.ChangeServings(servings, true);
                    
                    ComponentBuilderV2 newComponentContainer = BuildComponentUnsafe(recipe0);
                    
                    await modal.UpdateAsync(m => m.Components = newComponentContainer.Build());
                }
                
                break;
            default:
                return;
        }
    }

    private static ModalBuilder CreateServingsModal(short servings)
    {
        TextInputBuilder? textInput = new TextInputBuilder()
            .WithCustomId(RecipeServingsInput)
            .WithLabel("Servings")
            .WithValue(servings.ToString()) // Default value
            .WithMinLength(1)
            .WithMaxLength(3)
            .WithStyle(TextInputStyle.Short);

        return new ModalBuilder()
            .WithCustomId(RecipeServingsButton)
            .WithTitle("Set Servings")
            .AddTextInput(textInput);
    }

    private static ComponentBuilderV2 BuildComponentUnsafe(Recipe recipe, Units units = Units.Si)
    {
        StringBuilder ingredients = new($"## Ingredients{Environment.NewLine}");
        StringBuilder instructions = new($"## Instructions{Environment.NewLine}");
        ButtonBuilder servingsModalButton = new ButtonBuilder()
            .WithCustomId(RecipeServingsModal)
            .WithLabel("Set servings")
            .WithStyle(ButtonStyle.Primary);

        foreach (RecipeIngredient recipeIngredient in recipe.RecipeIngredients)
        {
            ingredients.Append($"`{recipeIngredient.Quantity} {recipeIngredient.Unit.ToSymbol()}` {recipeIngredient.Ingredient.Name} ");
 
            if (recipeIngredient.Optional)
                ingredients.AppendLine("[Optional]");
            else
                ingredients.Append(Environment.NewLine);
        }

        for (var i = 0; i < recipe.Instruction.Length; i++)
            instructions.AppendLine($"`{i + 1}.` {recipe.Instruction[i]}{Environment.NewLine}");
        
        return new ComponentBuilderV2()
            .WithTextDisplay($"# {recipe.Name}", RecipeNameDisplay)
            .WithTextDisplay($"-# {recipe.Servings} servings", RecipeServingsDisplay)
            .WithMediaGallery([
                "https://cdn.discordapp.com/attachments/964253122547552349/1336440069892083712/7Q3S.gif?ex=67a3d04e&is=67a27ece&hm=059c9d28466f43a50c4b450ca26fc01298a2080356421d8524384bf67ea8f3ab&"
            ])
            .WithActionRow([servingsModalButton])
            .WithTextDisplay(ingredients.ToString())
            .WithTextDisplay($"""
                              ## Oven Settings
                              Mode: `{recipe.OvenMode.ToHumanReadable()}`
                              Temperature: `{recipe.Temperature.Convert(Unit.Temperature, Units.Si, units)} {units.ToSymbol()}`
                              """)
            .WithActionRow([
                new SelectMenuBuilder(
                    RecipeUnitInput,
                    options:[
                        new SelectMenuOptionBuilder(
                            "Metric",
                            "1",
                            isDefault: units == Units.Metric),
                        new SelectMenuOptionBuilder(
                            "Imperial",
                            "2",
                            isDefault: units == Units.Imperial),
                        new SelectMenuOptionBuilder(
                            "Kelvin",
                            "0",
                            isDefault: units == Units.Si)
                    ],
                    id: RecipeUnitSelectMenu
                )
            ])
            .WithTextDisplay(instructions.ToString());
    }
    
    private async Task<ComponentBuilderV2> BuildComponentsUnsafeAsync()
    {
        if (!_recipes.Any())
        {
            return new ComponentBuilderV2()
                .WithTextDisplay(
                    """
                    # No recipes found
                    You should consider adding some.
                    """);
        }

        var builder = new ComponentBuilderV2();
        Emote? emote = await _clientProvider.Client.GetApplicationEmoteAsync(1393996479357517925);

        foreach (Recipe recipe in _recipes)
        {
            var buttonBuilder = new ButtonBuilder("Look inside", RecipesLookInsideButton);
            
            if (emote is not null)
                buttonBuilder.WithEmote(emote);

            builder
                .WithTextDisplay($"# {recipe.Name}")
                .WithMediaGallery(["https://cdn.discordapp.com/attachments/964253122547552349/1336440069892083712/7Q3S.gif?ex=67a3d04e&is=67a27ece&hm=059c9d28466f43a50c4b450ca26fc01298a2080356421d8524384bf67ea8f3ab&"])
                .WithActionRow([
                    buttonBuilder
                ]);
        }
        
        return builder;
    }

    private async Task<ComponentBuilderV2?> BuildComponentAsync(Recipe? recipe) =>
        recipe is null ? await BuildComponentsUnsafeAsync() : BuildComponentUnsafe(recipe);

    public async Task<ComponentBuilderV2?> GetRecipeComponentAsync(int recipeId) =>
        await BuildComponentAsync(_recipes.FirstOrDefault(r => r.RecipeId == recipeId));

    private async Task<ComponentBuilderV2?> GetRecipeComponentAsync(string recipeName) =>
        await BuildComponentAsync(_recipes.FirstOrDefault(r => r.Name == recipeName));

    public async Task<ComponentBuilderV2?> GetRecipesComponentAsync() =>
        await BuildComponentAsync(null);
}
