using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Services;
using Discord;
using Discord.Interactions;

namespace Adribot.Commands.Fun;

public class RecipeCommands(RecipeService recipeService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("recipe", "Gets a recipe")]
    [RequireBotPermission(ChannelPermission.SendMessages)]
    [RequireUserPermission(ChannelPermission.SendMessages)]
    public async Task GetRecipeAsync(RecipeName name)
    {
        MessageComponent? embed = (await recipeService.GetRecipeComponentAsync((int)name))?.Build();

        if (embed is null)
        {
            await RespondAsync($"No recipe found for this name", ephemeral: true);

            return;
        }
        
        await RespondAsync(components: embed);
    }
    
    [SlashCommand("recipes", "Gets all recipes")]
    [RequireBotPermission(ChannelPermission.SendMessages)]
    [RequireUserPermission(ChannelPermission.SendMessages)]
    public async Task GetRecipesAsync()
    {
        MessageComponent? embed = (await recipeService.GetRecipesComponentAsync())?.Build();

        if (embed is null)
        {
            await RespondAsync($"No recipes found.", ephemeral: true);

            return;
        }

        await RespondAsync(components: embed);
    }
}
