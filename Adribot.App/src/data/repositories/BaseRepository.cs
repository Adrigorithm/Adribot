using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data.repositories;

public abstract class BaseRepository(IDbContextFactory<AdribotContext> dbContextFactory)
{
    protected AdribotContext CreateDbContext() =>
        dbContextFactory.CreateDbContext();

    protected async Task<AdribotContext> CreateDbContextAsync() =>
        await dbContextFactory.CreateDbContextAsync();
}