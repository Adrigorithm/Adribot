using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Adribot.config;
using Adribot.constants.enums;
using Adribot.entities.fun.cat;
using Adribot.entities.fun.dog;
using Adribot.entities.fun.fox;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

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
                    Color = DiscordColor.MidnightBlue,
                    Title = "You asked, I delivered.",
                    ImageUrl = catApiObject[0].Url
                }));
                break;
            case AnimalType.DOG:
                List<Dog> dogApiObject = await JsonSerializer.DeserializeAsync<List<Dog>>(await _httpClient.GetStreamAsync($"{_dogUriBase}?api_key={Config.Configuration.CatToken}"));
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder
                {
                    Color = DiscordColor.MidnightBlue,
                    Title = "You asked, I delivered.",
                    ImageUrl = dogApiObject[0].Url
                }));
                break;
            case AnimalType.FOX:
                Fox foxApiObject = await JsonSerializer.DeserializeAsync<Fox>(await _httpClient.GetStreamAsync("https://randomfox.ca/floof"));
                await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(new DiscordEmbedBuilder
                {
                    Color = DiscordColor.MidnightBlue,
                    Title = "You asked, I delivered.",
                    ImageUrl = foxApiObject.Image
                }));
                break;
            default:
                break;
        }

        
    }
}