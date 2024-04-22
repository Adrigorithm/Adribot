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
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Adribot.src.commands.fun;

public class FunCommands(SecretsProvider _secretsProvider) : ApplicationCommandModule
{
    private readonly HttpClient _httpClient = new();

    [SlashCommand("get", "Gets a random animal")]
    public async Task GetAnimalAsync(InteractionContext ctx, [Option("animal", "pick your favourite floof")] AnimalType animal = AnimalType.Cat)
    {
        switch (animal)
        {
            case AnimalType.Cat:
                List<Cat> catApiObject = await JsonSerializer.DeserializeAsync<List<Cat>>(await _httpClient.GetStreamAsync($"{ConstantStrings.CatBaseUri}?api_key={_secretsProvider.Config.CatToken}"));
                await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder
                {
                    Color = new DiscordColor(_secretsProvider.Config.EmbedColour),
                    Title = "You asked, I delivered.",
                    ImageUrl = catApiObject[0].Url
                }));
                break;
            case AnimalType.Dog:
                List<Dog> dogApiObject = await JsonSerializer.DeserializeAsync<List<Dog>>(await _httpClient.GetStreamAsync($"{ConstantStrings.DogBaseUri}?api_key={_secretsProvider.Config.CatToken}"));
                await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder
                {
                    Color = new DiscordColor(_secretsProvider.Config.EmbedColour),
                    Title = "You asked, I delivered.",
                    ImageUrl = dogApiObject[0].Url
                }));
                break;
            case AnimalType.Fox:
                Fox foxApiObject = await JsonSerializer.DeserializeAsync<Fox>(await _httpClient.GetStreamAsync(ConstantStrings.FoxUri));
                await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder
                {
                    Color = new DiscordColor(_secretsProvider.Config.EmbedColour),
                    Title = "You asked, I delivered.",
                    ImageUrl = foxApiObject.Image
                }));
                break;
            default:
                break;
        }
    }

    [SlashCommand("pp", "Calculates your pp size")]
    public async Task GetPpSizeAsync(InteractionContext ctx, [Option("user", "user to calculate the pp size for")] DiscordUser user = null, [Option("unit", "unit to display the pp size in")] DistanceUnit unit = DistanceUnit.Inch)
    {
        var memberId = user is null ? ctx.Member.Id : user.Id;
        short sum = 0;

        memberId.ToString().ToList().ForEach(c => sum += (short)char.GetNumericValue(c));

        var ppSize = (short)(Math.Pow(memberId, 1.0 / sum) * 10 / char.GetNumericValue(memberId.ToString()[0]));
        if (unit == DistanceUnit.Inch)
            ppSize = (short)(ppSize / 2.5);

        await ctx.CreateResponseAsync(DiscordInteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent("<@" + (user?.Id.ToString() ?? ctx.Member.Id.ToString()) + "> Your pp size is " + Convert.ToString(ppSize) + (unit == DistanceUnit.Inch ? " inch" : " cm"))));
    }
}
