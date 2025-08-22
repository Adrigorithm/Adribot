using Adribot.entities.discord;
using Adribot.entities.fun;
using Adribot.entities.fun.pokemon;
using Adribot.entities.fun.recipe;
using Adribot.entities.utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.data;

public class AdribotContext(DbContextOptions<AdribotContext> options) : DbContext(options)
{
    public DbSet<DGuild> DGuilds { get; set; }
    public DbSet<DMember> DMembers { get; set; }

    public DbSet<Infraction> Infractions { get; set; }

    public DbSet<Reminder> Reminders { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<IcsCalendar> IcsCalendars { get; set; }
    public DbSet<Event> Events { get; set; }

    public DbSet<Starboard> Starboards { get; set; }
    public DbSet<MessageLink> MessageLinks { get; set; }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<WireConfig> WireConfigs { get; set; }

    public DbSet<Ability> Abilities { get; set; }
    public DbSet<AbilityEffectChange> AbilityEffectChanges { get; set; }
    public DbSet<AbilityFlavourText> AbilityFlavourTexts { get; set; }
    public DbSet<AbilityPokemon> AbilityPokemons { get; set; }
    public DbSet<ApiResource> ApiResources { get; set; }
    public DbSet<AwesomeName> AwesomeNames { get; set; }
    public DbSet<Berry> Berries { get; set; }
    public DbSet<BerryFirmness> BerryFirmnesses { get; set; }
    public DbSet<BerryFlavour> BerryFlavours { get; set; }
    public DbSet<BerryFlavourMap> BerryFlavourMaps { get; set; }
    public DbSet<ChainLink> ChainLinks { get; set; }
    public DbSet<Characteristic> Characteristics { get; set; }
    public DbSet<ContestComboDetail> ContestComboDetails { get; set; }
    public DbSet<ContestComboSets> ContestComboSetsEnumerable { get; set; }
    public DbSet<ContestEffect> ContestEffects { get; set; }
    public DbSet<ContestName> ContestNames { get; set; }
    public DbSet<ContestType> ContestTypes { get; set; }
    public DbSet<Description> Descriptions { get; set; }
    public DbSet<Effect> Effects { get; set; }
    public DbSet<EggGroup> EggGroups { get; set; }
    public DbSet<Encounter> Encounters { get; set; }
    public DbSet<EncounterCondition> EncounterConditions { get; set; }
    public DbSet<EncounterConditionValue> EncounterConditionValues { get; set; }
    public DbSet<EncounterMethod> EncounterMethods { get; set; }
    public DbSet<EncounterMethodRate> EncounterMethodRates { get; set; }
    public DbSet<EncounterVersionDetails> EncounterVersionDetailsEnumerable { get; set; }
    public DbSet<EvolutionChain> EvolutionChains { get; set; }
    public DbSet<EvolutionDetail> EvolutionDetails { get; set; }
    public DbSet<EvolutionTrigger> EvolutionTriggers { get; set; }
    public DbSet<FlavourBerryMap> FlavourBerryMaps { get; set; }
    public DbSet<FlavourText> FlavourTexts { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Generation> Generations { get; set; }
    public DbSet<GenerationGameIndex> GenerationGameIndices { get; set; }
    public DbSet<Genus> Genera { get; set; }
    public DbSet<GrowthRate> GrowthRates { get; set; }
    public DbSet<GrowthRateExperienceLevel> GrowthRateExperienceLevels { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemAttribute> ItemAttributes { get; set; }
    public DbSet<ItemCategory> ItemCategories { get; set; }
    public DbSet<ItemFlingEffect> ItemFlingEffects { get; set; }
    public DbSet<ItemHolderPokemon> ItemHolderPokemons { get; set; }
    public DbSet<ItemHolderPokemonVersionDetail> ItemHolderPokemonVersionDetails { get; set; }
    public DbSet<ItemPocket> ItemPockets { get; set; }
    public DbSet<ItemSprites> ItemSpritesEnumerable { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<LocationArea> LocationAreas { get; set; }
    public DbSet<LocationAreaEncounter> LocationAreaEncounters { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<MachineVersionDetail> MachineVersionDetails { get; set; }
    public DbSet<Move> Moves { get; set; }
    public DbSet<MoveAilment> MoveAilments { get; set; }
    public DbSet<MoveBattleStyle> MoveBattleStyles { get; set; }
    public DbSet<MoveBattleStylePreference> MoveBattleStylePreferences { get; set; }
    public DbSet<MoveCategory> MoveCategories { get; set; }
    public DbSet<MoveDamageClass> MoveDamageClasses { get; set; }
    public DbSet<MoveFlavourText> MoveFlavourTexts { get; set; }
    public DbSet<MoveLearnMethod> MoveLearnMethods { get; set; }
    public DbSet<MoveMetaData> MoveMetaDatas { get; set; }
    public DbSet<MoveStatAffect> MoveStatAffects { get; set; }
    public DbSet<MoveStatAffectSets> MoveStatAffectSetsEnumerable { get; set; }
    public DbSet<MoveStatChange> MoveStatChanges { get; set; }
    public DbSet<MoveTarget> MoveTargets { get; set; }
    public DbSet<Name> Names { get; set; }
    public DbSet<NamedApiResource> NamedApiResources { get; set; }
    public DbSet<Nature> Natures { get; set; }
    public DbSet<NaturePokeathlonStatAffect> NaturePokeathlonStatAffects { get; set; }
    public DbSet<NaturePokeathlonStatAffectSets> NaturePokeathlonStatAffectSetsEnumerable { get; set; }
    public DbSet<NatureStatAffectSets> NatureStatAffectSetsEnumerable { get; set; }
    public DbSet<NatureStatChange> NatureeStatChanges { get; set; }
    public DbSet<PalParkArea> PalParkAreas { get; set; }
    public DbSet<PalParkEncounterArea> PalParkEncounterAreas { get; set; }
    public DbSet<PalParkEncounterSpecies> PalParkEncounterSpeciesEnumerable { get; set; }
    public DbSet<PastMoveStatValues> PastMoveStatValuesEnumerable { get; set; }
    public DbSet<PokeathlonStat> PokeathlonStats { get; set; }
    public DbSet<Pokedex> Pokedexes { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<PokemonAbility> PokemonAbilities { get; set; }
    public DbSet<PokemonAbilityPast> PokemonAbilityPasts { get; set; }
    public DbSet<PokemonColour> PokemonColours { get; set; }
    public DbSet<PokemonCries> PokemonCriesEnumerable { get; set; }
    public DbSet<PokemonEncounter> PokemonEncounters { get; set; }
    public DbSet<PokemonEntry> PokemonEntries { get; set; }
    public DbSet<PokemonForm> PokemonForms { get; set; }
    public DbSet<PokemonFormSprites> PokemonFormSpritesEnumerable { get; set; }
    public DbSet<PokemonFormType> PokemonFormTypes { get; set; }
    public DbSet<PokemonHabitat> PokemonHabitats { get; set; }
    public DbSet<PokemonHeldItem> PokemonHeldItems { get; set; }
    public DbSet<PokemonHeldItemVersion> PokemonHeldItemVersions { get; set; }
    public DbSet<PokemonMove> PokemonMoves { get; set; }
    public DbSet<PokemonMoveVersion> PokemonMoveVersions { get; set; }
    public DbSet<PokemonShape> PokemonShapes { get; set; }
    public DbSet<PokemonSpecies> PokemonSpeciesEnumerable { get; set; }
    public DbSet<PokemonSpeciesDexEntry> PokemonSpeciesDexEntries { get; set; }
    public DbSet<PokemonSpeciesGender> PokemonSpeciesGenders { get; set; }
    public DbSet<PokemonSpeciesVariety> PokemonSpeciesVarieties { get; set; }
    public DbSet<PokemonSprites> PokemonSpritesEnumerable { get; set; }
    public DbSet<PokemonStat> PokemonStats { get; set; }
    public DbSet<PokemonType> PokemonTypes { get; set; }
    public DbSet<PokemonTypePast> PokemonTypePasts { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Stat> Stats { get; set; }
    public DbSet<SuperContestEffect> SuperContestEffects { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<TypePokemon> TypePokemons { get; set; }
    public DbSet<TypeRelations> TypeRelationsEnumerable { get; set; }
    public DbSet<TypeRelationsPast> TypeRelationsPasts { get; set; }
    public DbSet<VerboseEffect> VerboseEffects { get; set; }
    public DbSet<Version> Versions { get; set; }
    public DbSet<VersionEncounterDetail> VersionEncounterDetails { get; set; }
    public DbSet<VersionGameIndex> VersionGameIndices { get; set; }
    public DbSet<VersionGroup> VersionGroups { get; set; }
    public DbSet<VersionGroupFlavourText> VersionGroupFlavourTexts { get; set; }
}
