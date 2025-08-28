using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Adribot.constants.enums;
using Adribot.data.repositories.pokemon;
using Adribot.entities.fun.pokemon;
using Adribot.helpers;

namespace Adribot.services;

public class PokemonService(PokemonRepository pokemonRepository, IHttpClientFactory httpClientFactory)
{
    public async Task GetEverythingAsync()
    {
        await GetAllBerriesAsync();
        await GetAllBerryFirmnessesAsync();
    }

    private (string? endpoint, string? error) GetListEndpointString(PokemonEndpoint pokemonEndpoint) =>
        PokemonEndpointStringGenerator.GetEndpointString(new EndpointStringConfiguration(pokemonEndpoint, true, null, null));
    
    public async Task GetAllBerriesAsync()
    {
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.Berry);

        if (endpointString.error is not null)
            return;

        List<Berry>? berries = await JsonSerializer.DeserializeAsync<List<Berry>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        pokemonRepository.AddRange(berries);
    }
    
    public async Task GetAllBerryFirmnessesAsync()
    {
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.BerryFirmness);

        if (endpointString.error is not null)
            return;

        List<BerryFirmness>? berryFirmnesses = await JsonSerializer.DeserializeAsync<List<BerryFirmness>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        pokemonRepository.AddRange(berryFirmnesses);
    }
}
