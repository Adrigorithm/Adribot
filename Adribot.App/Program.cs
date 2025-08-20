using System;
using System.Threading.Tasks;
using Adribot.Data;
using Adribot.Data.Repositories;
using Adribot.Entities;
using Adribot.Extensions;
using Adribot.Services;
using Adribot.Services.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Adribot;

internal static class Program
{
    private static IServiceProvider _serviceProvider;

    // ReSharper disable once InconsistentNaming
    public static async Task Main(string[] args)
    {
        var secrets = SecretsProvider.LoadFromEnv();

        _serviceProvider = new ServiceCollection()
            .AddSingleton(secrets)
            .AddDbContextFactory<AdribotContext>(
                optionsAction: options => options.UseSqlServer(secrets.Config.SqlConnectionString)
            )
            .AddHttpClient()
            .AddSingleton<DGuildRepository>()
            .AddSingleton<DiscordClientProvider>()
            .AddSingleton<Bot>()
            .AddSingleton<WireRepository>()
            .AddSingleton<WireService>()
            .AddSingleton<RecipeRepository>()
            .AddSingleton<RecipeService>()
            .AddSingleton<StarboardRepository>()
            .AddSingleton<StarboardService>()
            .AddSingleton<ApplicationCommandService>()
            .AddSingleton<RemoteAccessService>()
            .AddSingleton<InfractionRepository>()
            .AddSingleton<RemindMeRepository>()
            .AddSingleton<IcsCalendarRepository>()
            .AddSingleton<TagRepository>()
            .AddSingleton<InfractionService>()
            .AddSingleton<RemindMeService>()
            .AddSingleton<IcsCalendarService>()
            .AddSingleton<TagService>()
            .BuildServiceProvider();

        await RunAsync();
    }

    private static async Task RunAsync()
    {
        IDbContextFactory<AdribotContext> contextFactory = _serviceProvider.GetRequiredService<IDbContextFactory<AdribotContext>>();

        await using (AdribotContext context = await contextFactory.CreateDbContextAsync())
        {
            await context.Database.MigrateAsync();
            await context.SeedIfEmptyAsync();
        }

        await _serviceProvider.GetRequiredService<Bot>().StartAsync(_serviceProvider.GetRequiredService<SecretsProvider>().Config.BotToken, _serviceProvider);

        await Task.Delay(-1);
    }
}
