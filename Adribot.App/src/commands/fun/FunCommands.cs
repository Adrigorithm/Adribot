using System;
using System.Collections.Generic;
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

public class FunCommands(IHttpClientFactory _httpClientFactory, SecretsProvider _secretsProvider) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("get", "Gets a random animal")]
    [RequireBotPermission(ChannelPermission.SendMessages)]
    [RequireUserPermission(ChannelPermission.SendMessages)]
    public async Task GetAnimalAsync(AnimalType animal = AnimalType.Cat)
    {
        EmbedBuilder embed = new();
        HttpClient httpClient = _httpClientFactory.CreateClient();

        switch (animal)
        {
            case AnimalType.Dog:
                List<Dog> dogApiObject = await JsonSerializer.DeserializeAsync<List<Dog>>(await httpClient.GetStreamAsync($"{ConstantStrings.DogBaseUri}?api_key={_secretsProvider.Config.CatToken}"));
                embed.ImageUrl = dogApiObject[0].Url;

                break;
            case AnimalType.Fox:
                Fox foxApiObject = await JsonSerializer.DeserializeAsync<Fox>(await httpClient.GetStreamAsync(ConstantStrings.FoxUri));
                embed.ImageUrl = foxApiObject.Image;

                break;
            default: // Cat
                List<Cat> catApiObject = await JsonSerializer.DeserializeAsync<List<Cat>>(await httpClient.GetStreamAsync($"{ConstantStrings.CatBaseUri}?api_key={_secretsProvider.Config.CatToken}"));
                embed.ImageUrl = catApiObject[0].Url;

                break;
        }

        embed.Color = _secretsProvider.Config.EmbedColour;
        embed.Title = "You asked, I delivered.";

        await RespondAsync(embed: embed.Build());
    }

    [SlashCommand("pp", "Calculates your pp size")]
    [RequireBotPermission(ChannelPermission.SendMessages)]
    [RequireUserPermission(ChannelPermission.SendMessages)]
    public async Task GetPpSizeAsync([Summary("user", "user to calculate the pp size for")] IUser user = null, [Summary("unit", "unit to display the pp size in")] DistanceUnit unit = DistanceUnit.Inch)
    {
        var memberId = user?.Id ?? Context.User.Id;
        var sum = SumOfDigits(memberId);

        static int SumOfDigits(ulong largeNumber)
        {
            var sum = 0;

            while (largeNumber > 0)
            {
                sum += (int)(largeNumber % 10);
                largeNumber /= 10;
            }

            return sum;
        }

        var memberIdLog10 = Math.Log10(memberId);
        var firstDigit = (short)Math.Pow(10, memberIdLog10 - (short)memberIdLog10);
        var ppSize = (short)(Math.Pow(memberId, 1.0 / sum) * 10 / firstDigit);

        if (unit == DistanceUnit.Inch)
            ppSize = (short)(ppSize / 2.5);

        await RespondAsync($"${user?.Mention ?? Context.User.Mention}, Your pp size is {Convert.ToString(ppSize)} {(unit == DistanceUnit.Inch ? " inch" : " cm")}");
    }
}
