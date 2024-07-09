using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.constants.strings;
using Adribot.src.entities.fun.cat;
using Adribot.src.entities.fun.dog;
using Adribot.src.entities.fun.fox;
using Adribot.src.services.providers;
using Discord;
using Discord.Interactions;

namespace Adribot.src.commands.fun;

public class FunCommands(SecretsProvider _secretsProvider) : InteractionModuleBase
{
    private readonly HttpClient _httpClient = new();

    [SlashCommand("get", "Gets a random animal")]
    public async Task GetAnimalAsync(AnimalType animal = AnimalType.Cat)
    {
        EmbedBuilder embed = new();

        switch (animal)
        {
            case AnimalType.Dog:
                List<Dog> dogApiObject = await JsonSerializer.DeserializeAsync<List<Dog>>(await _httpClient.GetStreamAsync($"{ConstantStrings.DogBaseUri}?api_key={_secretsProvider.Config.CatToken}"));
                embed.ImageUrl = dogApiObject[0].Url;

                break;
            case AnimalType.Fox:
                Fox foxApiObject = await JsonSerializer.DeserializeAsync<Fox>(await _httpClient.GetStreamAsync(ConstantStrings.FoxUri));
                embed.ImageUrl = foxApiObject.Image;

                break;
            default: // Cat
                List<Cat> catApiObject = await JsonSerializer.DeserializeAsync<List<Cat>>(await _httpClient.GetStreamAsync($"{ConstantStrings.CatBaseUri}?api_key={_secretsProvider.Config.CatToken}"));
                embed.ImageUrl = catApiObject[0].Url;
                
                break;
        }

        embed.Color = new Color(Convert.ToUInt32(_secretsProvider.Config.EmbedColour));
        embed.Title = "You asked, I delivered.";

        await RespondAsync(embed: embed.Build());
    }

    [SlashCommand("pp", "Calculates your pp size")]
    public async Task GetPpSizeAsync(InteractionContext ctx, [Summary("user", "user to calculate the pp size for")] IUser user = null, [Summary("unit", "unit to display the pp size in")] DistanceUnit unit = DistanceUnit.Inch)
    {
        var memberId = user?.Id ?? ctx.User.Id;
        short sum = 0;

        memberId.ToString().ToList().ForEach(c => sum += (short)char.GetNumericValue(c));

        var ppSize = (short)(Math.Pow(memberId, 1.0 / sum) * 10 / char.GetNumericValue(memberId.ToString()[0]));
       
       if (unit == DistanceUnit.Inch)
            ppSize = (short)(ppSize / 2.5);

        await RespondAsync($"${user?.Mention ?? ctx.User.Mention}, Your pp size is {Convert.ToString(ppSize)} {(unit == DistanceUnit.Inch ? " inch" : " cm")}");
    }
}
