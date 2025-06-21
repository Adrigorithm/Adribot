using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Constants.Strings;
using Adribot.Entities.Fun.Cat;
using Adribot.Entities.Fun.Dog;
using Adribot.Entities.Fun.Fox;
using Adribot.Services;
using Adribot.Services.Providers;
using Discord;
using Discord.Interactions;

namespace Adribot.Commands.Fun;

public class RecipeCommands(RecipeService recipeService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("recipe", "Gets arecipe")]
    [RequireBotPermission(ChannelPermission.SendMessages)]
    [RequireUserPermission(ChannelPermission.SendMessages)]
    public async Task GetAnimalAsync(RecipeName name)
    {
        ComponentBuilderV2? embed = recipeService.GetRecipe((int)name);
        
        if  (embed is null)
            return;
        
        await RespondAsync(components: embed.Build());
    }
}
