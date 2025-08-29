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
    
    public async Task<List<BerryFirmness>?> GetAllBerryFirmnessesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<BerryFirmness>(shouldAvoidCache, PokemonEndpoint.BerryFirmness, true);
    
    public async Task<List<BerryFlavour>?> GetAllBerryFlavoursAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<BerryFlavour>(shouldAvoidCache, PokemonEndpoint.BerryFlavour, true);
    
    public async Task<List<ContestType>?> GetAllContestTypesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<ContestType>(shouldAvoidCache, PokemonEndpoint.ContestType, true);
    
    public async Task<List<ContestEffect>?> GetAllContestEffectsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<ContestEffect>(shouldAvoidCache, PokemonEndpoint.ContestEffect, false);
    
    public async Task<List<SuperContestEffect>?> GetAllSuperContestEffectsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<SuperContestEffect>(shouldAvoidCache, PokemonEndpoint.SuperContestEffect, false);
    
    public async Task<List<EncounterMethod>?> GetAllEncounterMethodsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<EncounterMethod>(shouldAvoidCache, PokemonEndpoint.EncounterMethod, true);
    
    public async Task<List<EncounterCondition>?> GetAllEncounterConditionsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<EncounterCondition>(shouldAvoidCache, PokemonEndpoint.EncounterCondition, true);
    
    public async Task<List<EncounterConditionValue>?> GetAllEncounterConditionValuesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<EncounterConditionValue>(shouldAvoidCache, PokemonEndpoint.EncounterConditionValue, true);
    
    public async Task<List<EvolutionChain>?> GetAllEvolutionChainsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<EvolutionChain>(shouldAvoidCache, PokemonEndpoint.EvolutionChain, false);
    
    public async Task<List<EvolutionTrigger>?> GetAllEvolutionTriggersAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<EvolutionTrigger>(shouldAvoidCache, PokemonEndpoint.EvolutionTrigger, true);
    
    public async Task<List<Generation>?> GetAllGenerationsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Generation>(shouldAvoidCache, PokemonEndpoint.Generation, true);
    
    public async Task<List<Pokedex>?> GetAllPokedexesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Pokedex>(shouldAvoidCache, PokemonEndpoint.Pokedex, true);
    
    public async Task<List<Version>?> GetAllVersionsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Version>(shouldAvoidCache, PokemonEndpoint.Version, true);
    
    public async Task<List<VersionGroup>?> GetAllVersionGroupsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<VersionGroup>(shouldAvoidCache, PokemonEndpoint.VersionGroup, true);
    
    public async Task<List<Item>?> GetAllItemsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Item>(shouldAvoidCache, PokemonEndpoint.Item, true);
    
    public async Task<List<ItemAttribute>?> GetAllItemAttributesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<ItemAttribute>(shouldAvoidCache, PokemonEndpoint.ItemAttribute, true);
    
    public async Task<List<ItemCategory>?> GetAllItemCategoriesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<ItemCategory>(shouldAvoidCache, PokemonEndpoint.ItemCategory, true);
    
    public async Task<List<ItemFlingEffect>?> GetAllItemFlingEffectsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<ItemFlingEffect>(shouldAvoidCache, PokemonEndpoint.ItemFlingEffect, true);
    
    public async Task<List<ItemPocket>?> GetAllItemPocketsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<ItemPocket>(shouldAvoidCache, PokemonEndpoint.ItemPocket, true);
    
    public async Task<List<Location>?> GetAllLocationsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Location>(shouldAvoidCache, PokemonEndpoint.Location, true);
    
    public async Task<List<LocationArea>?> GetAllLocationAreasAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<LocationArea>(shouldAvoidCache, PokemonEndpoint.LocationArea, true);
    
    public async Task<List<PalParkArea>?> GetAllPalParkAreasAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<PalParkArea>(shouldAvoidCache, PokemonEndpoint.PalParkArea, true);
    
    public async Task<List<Region>?> GetAllRegionsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Region>(shouldAvoidCache, PokemonEndpoint.Region, true);
    
    public async Task<List<Machine>?> GetAllMachinesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Machine>(shouldAvoidCache, PokemonEndpoint.Machine, false);
    
    public async Task<List<Move>?> GetAllMovesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Move>(shouldAvoidCache, PokemonEndpoint.Move, true);
    
    public async Task<List<MoveAilment>?> GetAllMoveAilmentsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<MoveAilment>(shouldAvoidCache, PokemonEndpoint.MoveAilment, true);
    
    public async Task<List<MoveBattleStyle>?> GetAllMoveBattleStylesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<MoveBattleStyle>(shouldAvoidCache, PokemonEndpoint.MoveBattleStyle, true);
    
    public async Task<List<MoveCategory>?> GetAllMoveCategoriesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<MoveCategory>(shouldAvoidCache, PokemonEndpoint.MoveCategory, true);
    
    public async Task<List<MoveDamageClass>?> GetAllMoveDamageClassesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<MoveDamageClass>(shouldAvoidCache, PokemonEndpoint.MoveDamageClass, true);
    
    public async Task<List<MoveLearnMethod>?> GetAllMoveLearnMethodsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<MoveLearnMethod>(shouldAvoidCache, PokemonEndpoint.MoveLearnMethod, true);
    
    public async Task<List<MoveTarget>?> GetAllMoveTargetsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<MoveTarget>(shouldAvoidCache, PokemonEndpoint.MoveTarget, true);
    
    public async Task<List<Ability>?> GetAllAbilitiesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Ability>(shouldAvoidCache, PokemonEndpoint.Ability, true);
    
    public async Task<List<Characteristic>?> GetAllCharacteristicsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Characteristic>(shouldAvoidCache, PokemonEndpoint.Characteristic, false);
    
    public async Task<List<EggGroup>?> GetAllEggGroupsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<EggGroup>(shouldAvoidCache, PokemonEndpoint.EggGroup, true);
    
    public async Task<List<Gender>?> GetAllGenusAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Gender>(shouldAvoidCache, PokemonEndpoint.Gender, true);
    
    public async Task<List<GrowthRate>?> GetAllGrowthRatesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<GrowthRate>(shouldAvoidCache, PokemonEndpoint.GrowthRate, true);
    
    public async Task<List<Nature>?> GetAllNaturesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Nature>(shouldAvoidCache, PokemonEndpoint.Nature, true);
    
    public async Task<List<PokeathlonStat>?> GetAllPokeathlonStatsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<PokeathlonStat>(shouldAvoidCache, PokemonEndpoint.PokeathlonStat, true);
    
    public async Task<List<Pokemon>?> GetAllPokemonAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Pokemon>(shouldAvoidCache, PokemonEndpoint.Pokemon, true);
    
    public async Task<List<LocationAreaEncounter>?> GetAllPokemonLocationAreasAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<LocationAreaEncounter>(shouldAvoidCache, PokemonEndpoint.PokemonLocationArea, true);
    
    public async Task<List<PokemonColour>?> GetAllPokemonColoursAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<PokemonColour>(shouldAvoidCache, PokemonEndpoint.PokemonColour, true);
    
    public async Task<List<PokemonForm>?> GetAllPokemonFormsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<PokemonForm>(shouldAvoidCache, PokemonEndpoint.PokemonForm, true);
    
    public async Task<List<PokemonHabitat>?> GetAllPokemonHabitatsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<PokemonHabitat>(shouldAvoidCache, PokemonEndpoint.PokemonHabitat, true);
    
    public async Task<List<PokemonShape>?> GetAllPokemonShapesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<PokemonShape>(shouldAvoidCache, PokemonEndpoint.PokemonShape, true);
    
    public async Task<List<PokemonSpecies>?> GetAllPokemonSpeciesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<PokemonSpecies>(shouldAvoidCache, PokemonEndpoint.PokemonSpecies, true);
    
    public async Task<List<Stat>?> GetAllStatsAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Stat>(shouldAvoidCache, PokemonEndpoint.Stat, true);
    
    public async Task<List<Type>?> GetAllTypesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Type>(shouldAvoidCache, PokemonEndpoint.Type, true);
    
    public async Task<List<Language>?> GetAllLanguagesAsync(bool shouldAvoidCache = false) =>
        await GetAllAsync<Language>(shouldAvoidCache, PokemonEndpoint.Language, true);
}
