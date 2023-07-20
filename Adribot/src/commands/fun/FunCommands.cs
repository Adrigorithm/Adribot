using Adribot.config;
using Adribot.constants.enums;
using Adribot.entities.fun.cat;
using Adribot.entities.fun.dog;
using Adribot.entities.fun.fox;
using Adribot.src.constants.enums;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Adribot.commands.fun;

public class FunCommands : ApplicationCommandModule
{
    private readonly string _catUriBase = "https://api.thecatapi.com/v1/images/search";
    private readonly string _dogUriBase = "https://api.thedogapi.com/v1/images/search";
    private readonly HttpClient _httpClient = new();

    [SlashCommand("get", "Gets a random animal")]
    public async Task GetAnimalAsync(InteractionContext ctx, [Option("animal", "pick your favourite floof")] AnimalType animal = AnimalType.CAT)
    {
        switch (animal)
        {
            case AnimalType.CAT:
                List<Cat> catApiObject = await JsonSerializer.DeserializeAsync<List<Cat>>(await _httpClient.GetStreamAsync($"{_catUriBase}?api_key={Config.Configuration.CatToken}"));
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder
                {
                    Color = new DiscordColor(Config.Configuration.EmbedColour),
                    Title = "You asked, I delivered.",
                    ImageUrl = catApiObject[0].Url
                }));
                break;
            case AnimalType.DOG:
                List<Dog> dogApiObject = await JsonSerializer.DeserializeAsync<List<Dog>>(await _httpClient.GetStreamAsync($"{_dogUriBase}?api_key={Config.Configuration.CatToken}"));
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder
                {
                    Color = new DiscordColor(Config.Configuration.EmbedColour),
                    Title = "You asked, I delivered.",
                    ImageUrl = dogApiObject[0].Url
                }));
                break;
            case AnimalType.FOX:
                Fox foxApiObject = await JsonSerializer.DeserializeAsync<Fox>(await _httpClient.GetStreamAsync("https://randomfox.ca/floof"));
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder
                {
                    Color = new DiscordColor(Config.Configuration.EmbedColour),
                    Title = "You asked, I delivered.",
                    ImageUrl = foxApiObject.Image
                }));
                break;
            default:
                break;
        }
    }

    [SlashCommand("pp", "Calculates your pp size")]
    public async Task GetPPSizeAsync(InteractionContext ctx, [Option("user", "user to calculate the pp size for")] DiscordUser user = null, [Option("unit", "unit to display the pp size in")] DistanceUnit unit = DistanceUnit.INCH)
    {
        ulong memberId = user is null ? ctx.Member.Id : user.Id;
        short sum = 0;

        memberId.ToString().ToList().ForEach(c => sum += (short)char.GetNumericValue(c));

        short ppSize = (short)(Math.Pow(memberId, 1.0 / sum) * 10 / char.GetNumericValue(memberId.ToString()[0]));
        if (unit == DistanceUnit.INCH)
            ppSize = (short)(ppSize / 2.5);

        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(new DiscordMessageBuilder().WithContent("<@" + (user?.Id.ToString() ?? ctx.Member.Id.ToString()) + "> Your pp size is " + Convert.ToString(ppSize) + (unit == DistanceUnit.INCH ? " inch" : " cm"))));
    }
}
