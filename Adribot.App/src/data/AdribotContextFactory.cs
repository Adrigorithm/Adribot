using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Adribot.src.data;

public class AdribotContextFactory : IDesignTimeDbContextFactory<AdribotContext>
{
    public AdribotContext CreateDbContext(string [] args)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AdribotContext>();
        _ = optionsBuilder.UseSqlServer(config["DB_CONNECTION"] ?? throw new ArgumentNullException(null, "Enviroment variable not found: Adribot_sqlConnectionString"));

        return new AdribotContext(optionsBuilder.Options);
    }
}