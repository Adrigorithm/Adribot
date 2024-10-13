using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Adribot.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AdribotContext>
{
    public AdribotContext CreateDbContext(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AdribotContext>();
        optionsBuilder.UseSqlServer(config["DB_CONNECTION"]);

        return new AdribotContext(optionsBuilder.Options);
    }
}
