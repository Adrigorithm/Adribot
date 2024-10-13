using System.Threading.Tasks;
using Adribot.Data;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Data.Repositories;

public abstract class BaseRepository(IDbContextFactory<AdribotContext> dbContextFactory)
{
    protected AdribotContext CreateDbContext() =>
        dbContextFactory.CreateDbContext();

    protected async Task<AdribotContext> CreateDbContextAsync() =>
        await dbContextFactory.CreateDbContextAsync();
}
