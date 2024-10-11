using System;
using System.Threading.Tasks;
using Adribot.src.data;
using Adribot.src.data.repositories;
using Adribot.src.entities;
using Adribot.src.services;
using Adribot.src.services.providers;
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
        Console.WriteLine(secrets.Config);

        _serviceProvider = new ServiceCollection()
            .AddSingleton(secrets)
            .AddDbContextFactory<AdribotContext>(
                optionsAction: (options) => options.UseSqlServer(secrets.Config.SqlConnectionString)
            )
            .AddHttpClient()
            .AddSingleton<DGuildRepository>()
            .AddSingleton<DiscordClientProvider>()
            .AddSingleton<Bot>()
            .AddSingleton<RemoteAccessService>()
            .AddSingleton<InfractionRepository>()
            .AddSingleton<RemindMeRepository>()
            .AddSingleton<IcsCalendarRepository>()
            .AddSingleton<TagRepository>()
            .AddSingleton<InfractionService>()
            .AddSingleton<RemindMeSerivce>()
            .AddSingleton<IcsCalendarService>()
            .AddSingleton<StarboardService>()
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
        }

        await _serviceProvider.GetRequiredService<Bot>().StartAsync(_serviceProvider.GetRequiredService<SecretsProvider>().Config.BotToken, _serviceProvider);

        await Task.Delay(-1);
    }
}
