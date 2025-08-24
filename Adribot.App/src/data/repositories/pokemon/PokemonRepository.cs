using Microsoft.EntityFrameworkCore;

namespace Adribot.data.repositories.pokemon;

public sealed class PokemonRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{

}
