using Adribot.constants.enums;

namespace Adribot.helpers;

public static class PokemonEndpointStringGenerator
{
    private const string PokApiBaseUrl = "https://pokeapi.co/api/v2";
    private const int Limit = int.MaxValue;

    public static (string endpoint, string error) GetEndpointString(EndpointStringConfiguration config) =>
        GetString(config);

    private static string CreateRealUrl(string endpoint, bool fetchAll = false) =>
        $"{PokApiBaseUrl}{endpoint}{(fetchAll ? $"?limit={Limit}" : "")}";
    
    /// <summary>
    /// Creates the string used to connect to the PokéAPI and fetch the requested resource,
    /// as defined in the <b>EndpointStringConfiguration</b> object.
    /// </summary>
    /// <param name="config">configuration of the requested resource</param>
    /// <returns>
    /// a tuple of the composed endpoint string (or null, in which case, error will have a value)
    /// </returns>
    private static (string endpoint, string error) GetString(EndpointStringConfiguration config)
    {
        string id;
        var isIdNumeric = false;

        if (config.Id is null || config.Id <= 0)
            id = config.Name;
        else
        {
            id = config.Id.ToString();
            isIdNumeric = true;
        }
            
        var fetchAll = id is null || (config.ShouldFetchAll ?? false);
        string? endpoint = null;
        string? error = null;
        
        switch (config.Endpoint)
        {
            case PokemonEndpoint.Berry:
                endpoint = $"/berry{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.BerryFirmness:
                endpoint = $"/berry-firmness{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.BerryFlavour:
                endpoint = $"/berry-flavor{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.ContestType:
                endpoint = $"/contest-type{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.ContestEffect:
                if (!isIdNumeric)
                    error = $"Endpoint `contest-effect` requires a numeric id to be set when fetching a specific entity, but found: {id}";
                else
                    endpoint = $"/contest-effect{(fetchAll ? "" : $"/{id}")}";
                
                break;
            case PokemonEndpoint.SuperContestEffect:
                if (!isIdNumeric)
                    error = $"Endpoint `contest-effect` requires a numeric id to be set when fetching a specific entity, but found: {id}";
                else
                    endpoint = $"/super-contest-effect{(fetchAll ? "" : $"/{id}")}";
                
                break;
            case PokemonEndpoint.EncounterMethod:
                endpoint = $"/encounter-method{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.EncounterCondition:
                endpoint = $"/encounter-condition{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.EncounterConditionValue:
                endpoint = $"/encounter-condition-value{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.EvolutionChain:
                if (!isIdNumeric)
                    error = $"Endpoint `contest-effect` requires a numeric id to be set when fetching a specific entity, but found: {id}";
                else
                    endpoint = $"/evolution-chain{(fetchAll ? "" : $"/{id}")}";
                
                break;
            case PokemonEndpoint.EvolutionTrigger:
                endpoint = $"/evolution-trigger{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Generation:
                endpoint = $"/generation{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Pokedex:
                endpoint = $"/pokedex{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Version:
                endpoint = $"/version{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.VersionGroup:
                endpoint = $"/version-group{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Item:
                endpoint = $"/item{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.ItemAttribute:
                endpoint = $"/item-attribute{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.ItemCategory:
                endpoint = $"/item-category{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.ItemFlingEffect:
                endpoint = $"/item-fling-effect{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.ItemPocket:
                endpoint = $"/item-pocket{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Location:
                endpoint = $"/location{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.LocationArea:
                endpoint = $"/location-area{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.PalParkArea:
                endpoint = $"/pal-park-area{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Region:
                endpoint = $"/region{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Machine:
                if (!isIdNumeric)
                    error = $"Endpoint `contest-effect` requires a numeric id to be set when fetching a specific entity, but found: {id}";
                else
                    endpoint = $"/machine{(fetchAll ? "" : $"/{id}")}";
                
                break;
            case PokemonEndpoint.Move:
                endpoint = $"/move{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.MoveAilment:
                endpoint = $"/move-ailment{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.MoveBattleStyle:
                endpoint = $"/move-battle-style{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.MoveCategory:
                endpoint = $"/move-category{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.MoveDamageClass:
                endpoint = $"/move-damage-class{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.MoveLearnMethod:
                endpoint = $"/move-learn-method{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.MoveTarget:
                endpoint = $"/move-target{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Ability:
                endpoint = $"/ability{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Characteristic:
                if (!isIdNumeric)
                    error = $"Endpoint `contest-effect` requires a numeric id to be set when fetching a specific entity, but found: {id}";
                else
                    endpoint = $"/characteristic{(fetchAll ? "" : $"/{id}")}";
                
                break;
            case PokemonEndpoint.EggGroup:
                endpoint = $"/egg-group{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Gender:
                endpoint = $"/gender{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.GrowthRate:
                endpoint = $"/growth-rate{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Nature:
                endpoint = $"/nature{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.PokeathlonStat:
                endpoint = $"/pokeathlon-stat{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Pokemon:
                endpoint = $"/pokemon{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.PokemonLocationArea:
                endpoint = $"/pokemon/{(fetchAll ? "" : $"/{id}")}/encounters";

                break;
            case PokemonEndpoint.PokemonColour:
                endpoint = $"/pokemon-color{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.PokemonForm:
                endpoint = $"/pokemon-form{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.PokemonHabitat:
                endpoint = $"/pokemon-habitat{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.PokemonShape:
                endpoint = $"/pokemon-shape{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.PokemonSpecies:
                endpoint = $"/pokemon-species{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Stat:
                endpoint = $"/stat{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Type:
                endpoint = $"/type{(fetchAll ? "" : $"/{id}")}";

                break;
            case PokemonEndpoint.Language:
                endpoint = $"/language{(fetchAll ? "" : $"/{id}")}";

                break;
            default:
                error = $"No endpoint registered for {config.Endpoint}";

                break;
        }

        if (endpoint is not null)
            endpoint = CreateRealUrl(endpoint);
        
        return (endpoint, error);
    }
}
