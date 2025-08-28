using System.Collections.Generic;
using System.Linq;
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

    }

    private async Task<List<T>?> GetAllAsync<T>(bool shouldAvoidCache, PokemonEndpoint endpoint, bool isNamedApiResource) where T : class
    {
        if (!shouldAvoidCache)
            return pokemonRepository.GetAll<T>();
        
        (string? endpoint, string? error) endpointString = GetListEndpointString(endpoint);

        if (endpointString.error is not null)
            return null;

        var actualResourceList = new List<T>();
        
        if (isNamedApiResource)
        {
            NamedApiResourceList? resourceList = await JsonSerializer.DeserializeAsync<NamedApiResourceList>(
                await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));

            foreach (NamedApiResource resource in resourceList.Results)
                actualResourceList.Add(await GetActualNamedApiResourceAsync<T>(resource));
        }
        else
        {
            ApiResourceList? resourceList = await JsonSerializer.DeserializeAsync<ApiResourceList>(
                await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));

            foreach (ApiResource resource in resourceList.Results)
                actualResourceList.Add(await GetActualApiResourceAsync<T>(resource));
        }
        
        pokemonRepository.AddRange(actualResourceList);
        
        return actualResourceList;
    }

    private async Task<T> GetActualNamedApiResourceAsync<T>(NamedApiResource resource) =>
        await JsonSerializer.DeserializeAsync<T>(await httpClientFactory.CreateClient().GetStreamAsync(resource.Url));
    
    private async Task<T> GetActualApiResourceAsync<T>(ApiResource resource) =>
        await JsonSerializer.DeserializeAsync<T>(await httpClientFactory.CreateClient().GetStreamAsync(resource.Url));

    private (string? endpoint, string? error) GetListEndpointString(PokemonEndpoint pokemonEndpoint) =>
        PokemonEndpointStringGenerator.GetEndpointString(new EndpointStringConfiguration(pokemonEndpoint, true, null, null));
    
    public async Task<List<Berry>?> GetAllBerriesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Berry>(shouldAvoidCache, PokemonEndpoint.Berry, true);
    
    public async Task<List<BerryFirmness>?> GetAllBerryFirmnessesAsync(bool shouldAvoidCache = false)
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
    
    public async Task<List<BerryFlavour>?> GetAllBerryFlavoursAsync(bool shouldAvoidCache = false)
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
    
    public async Task<List<ContestType>?> GetAllContestTypesAsync(bool shouldAvoidCache = false)
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
    
    public async Task<List<ContestEffect>?> GetAllContestEffectsAsync(bool shouldAvoidCache = false)
    {
        if (!shouldAvoidCache)
            return pokemonRepository.GetAll<ContestEffect>();
        
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.ContestEffect);

        if (endpointString.error is not null)
            return null;

        List<ContestEffect>? contestEffects = await JsonSerializer.DeserializeAsync<List<ContestEffect>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        return pokemonRepository.AddRange(contestEffects);
    }
    
    public async Task<List<SuperContestEffect>?> GetAllSuperContestEffectsAsync(bool shouldAvoidCache = false)
    {
        if (!shouldAvoidCache)
            return pokemonRepository.GetAll<SuperContestEffect>();
        
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.SuperContestEffect);

        if (endpointString.error is not null)
            return null;

        List<SuperContestEffect>? superContestEffect = await JsonSerializer.DeserializeAsync<List<SuperContestEffect>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        return pokemonRepository.AddRange(superContestEffect);
    }
    
    public async Task<List<EncounterMethod>?> GetAllEncounterMethodsAsync(bool shouldAvoidCache = false)
    {
        if (!shouldAvoidCache)
            return pokemonRepository.GetAll<EncounterMethod>();
        
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.EncounterMethod);

        if (endpointString.error is not null)
            return null;

        List<EncounterMethod>? encounterMethod = await JsonSerializer.DeserializeAsync<List<EncounterMethod>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        return pokemonRepository.AddRange(encounterMethod);
    }
    
    public async Task<List<EncounterCondition>?> GetAllEncounterConditionsAsync(bool shouldAvoidCache = false)
    {
        if (!shouldAvoidCache)
            return pokemonRepository.GetAll<EncounterCondition>();
        
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.EncounterCondition);

        if (endpointString.error is not null)
            return null;

        List<EncounterCondition>? encounterCondition = await JsonSerializer.DeserializeAsync<List<EncounterCondition>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        return pokemonRepository.AddRange(encounterCondition);
    }
    
    public async Task<List<EncounterConditionValue>?> GetAllEncounterConditionValuesAsync(bool shouldAvoidCache = false)
    {
        if (!shouldAvoidCache)
            return pokemonRepository.GetAll<EncounterConditionValue>();
        
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.EncounterConditionValue);

        if (endpointString.error is not null)
            return null;

        List<EncounterConditionValue>? encounterConditionValue = await JsonSerializer.DeserializeAsync<List<EncounterConditionValue>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        return pokemonRepository.AddRange(encounterConditionValue);
    }
    
    public async Task<List<EvolutionChain>?> GetAllEvolutionChainsAsync(bool shouldAvoidCache = false)
    {
        if (!shouldAvoidCache)
            return pokemonRepository.GetAll<EvolutionChain>();
        
        (string? endpoint, string? error) endpointString = GetListEndpointString(PokemonEndpoint.EvolutionChain);

        if (endpointString.error is not null)
            return null;

        List<EvolutionChain>? evolutionChain = await JsonSerializer.DeserializeAsync<List<EvolutionChain>>(
            await httpClientFactory.CreateClient().GetStreamAsync(endpointString.endpoint));
        
        return pokemonRepository.AddRange(evolutionChain);
    }
}
