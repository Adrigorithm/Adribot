using System;
using Adribot.constants.enums;

namespace Adribot.helpers;

public static class PokemonEndpointStringGenerator
{
    private const string PokApiBaseUrl = "https://pokeapi.co/api/v2";

    public static string GetEndpointString(EndpointStringConfiguration config) =>
        GetString(config);

    private static string GetString(EndpointStringConfiguration config) =>
        config.Endpoint switch
        {
            PokemonEndpoint.Berry => "Berry",
            PokemonEndpoint.BerryFirmness => "berry-firmness",
            PokemonEndpoint.BerryFlavour => "berry-flavor",
            PokemonEndpoint.ContestType => "contest-type",
            PokemonEndpoint.ContestEffect => "contest-effect",
            PokemonEndpoint.SuperContestEffect => "super-contest-effect",
            PokemonEndpoint.EncounterMethod => "encounter-method",
            PokemonEndpoint.EncounterCondition => "encounter-condition",
            PokemonEndpoint.EncounterConditionValue => "encounter-condition-value",
            PokemonEndpoint.EvolutionChain => "evolution-chain",
            PokemonEndpoint.EvolutionTrigger => "evolution-trigger",
            PokemonEndpoint.Generation => "generation",
            PokemonEndpoint.Pokedex => "pokedex",
            PokemonEndpoint.Version => "version",
            PokemonEndpoint.VersionGroup => "version-group",
            PokemonEndpoint.Item => "item",
            PokemonEndpoint.ItemAttribute => "item-attribute",
            PokemonEndpoint.ItemCategory => "item-category",
            PokemonEndpoint.ItemFlingEffect => "item-fling-effect",
            PokemonEndpoint.ItemPocket => "item-pocket",
            PokemonEndpoint.Location => "location",
            PokemonEndpoint.LocationArea => "location-area",
            PokemonEndpoint.PalParkArea => "pal-park-area",
            PokemonEndpoint.Region => "region",
            PokemonEndpoint.Machine => "machine",
            PokemonEndpoint.Move => "move",
            PokemonEndpoint.MoveAilment => "move-ailment",
            PokemonEndpoint.MoveBattleStyle => "move-battle-style",
            PokemonEndpoint.MoveCategory => "move-category",
            PokemonEndpoint.MoveDamageClass => "move-damage-class",
            PokemonEndpoint.MoveLearnMethod => "move-learn-method",
            PokemonEndpoint.MoveTarget => "move-target",
            PokemonEndpoint.Ability => "ability",
            PokemonEndpoint.Characteristic => "characteristic",
            PokemonEndpoint.EggGroup => "egg-group",
            PokemonEndpoint.Gender => "gender",
            PokemonEndpoint.GrowthRate => "growth-rate",
            PokemonEndpoint.Nature => "nature",
            PokemonEndpoint.PokeathlonStat => "pokeathlon-stat",
            PokemonEndpoint.Pokemon => "pokemon",
            PokemonEndpoint.PokemonLocationArea => "pokemon",
            PokemonEndpoint.PokemonColour => "pokemon-color",
            PokemonEndpoint.PokemonForm => "pokemon-form",
            PokemonEndpoint.PokemonHabitat => "pokemon-habitat",
            PokemonEndpoint.PokemonShape => "pokemon-shape",
            PokemonEndpoint.PokemonSpecies => "pokemon-species",
            PokemonEndpoint.Stat => "stat",
            PokemonEndpoint.Type => "type",
            PokemonEndpoint.Language => "language",

            _ => throw new ArgumentOutOfRangeException(nameof(config.Endpoint), config.Endpoint, null)
        };
}
