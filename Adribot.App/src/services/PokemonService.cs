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
    public async Task CacheEverythingAsync()
    {
        _ = await GetAllBerriesAsync(true);
        _ = await GetAllBerryFirmnessesAsync(true);
        _ = await GetAllBerryFlavoursAsync(true);
       _ =  await GetAllContestTypesAsync(true);
    }

    private (string? endpoint, string? error) GetListEndpointString(PokemonEndpoint pokemonEndpoint) =>
        PokemonEndpointStringGenerator.GetEndpointString(new EndpointStringConfiguration(pokemonEndpoint, true, null, null));
    
    public async Task<List<Berry>>? GetAllBerriesAsync(bool shouldAvoidCache = false)
    {
        if (!shouldAvoidCache)
            return pokemonRepository.GetAll<Berry>();
        
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.Berry);

        if (endpointString.error is not null)
            return null;

        List<Berry>? berries = await JsonSerializer.DeserializeAsync<List<Berry>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        return pokemonRepository.AddRange(berries);
    }
    
    public async Task<List<BerryFirmness>>? GetAllBerryFirmnessesAsync(bool shouldAvoidCache = false)
    {
        if (!shouldAvoidCache)
            return pokemonRepository.GetAll<BerryFirmness>();
        
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.BerryFirmness);

        if (endpointString.error is not null)
            return null;

        List<BerryFirmness>? berryFirmnesses = await JsonSerializer.DeserializeAsync<List<BerryFirmness>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        return pokemonRepository.AddRange(berryFirmnesses);
    }
    
    public async Task<List<BerryFlavour>>? GetAllBerryFlavoursAsync(bool shouldAvoidCache = false)
    {
        if (!shouldAvoidCache)
            return pokemonRepository.GetAll<BerryFlavour>();
        
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.BerryFlavour);

        if (endpointString.error is not null)
            return null;

        List<BerryFlavour>? berryFlavours = await JsonSerializer.DeserializeAsync<List<BerryFlavour>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        return pokemonRepository.AddRange(berryFlavours);
    }
    
    public async Task<List<ContestType>>? GetAllContestTypesAsync(bool shouldAvoidCache = false)
    {
        if (!shouldAvoidCache)
            return pokemonRepository.GetAll<ContestType>();
        
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.ContestType);

        if (endpointString.error is not null)
            return null;

        List<ContestType>? contestTypes = await JsonSerializer.DeserializeAsync<List<ContestType>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        return pokemonRepository.AddRange(contestTypes);
    }
}
