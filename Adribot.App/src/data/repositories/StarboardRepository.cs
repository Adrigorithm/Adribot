using System.Collections.Frozen;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Data.Repositories;

public sealed class StarboardRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{
    public 
}
