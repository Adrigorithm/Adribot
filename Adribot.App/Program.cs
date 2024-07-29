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

    public static async Task Main(string[] args)
    {
        var secrets = SecretsProvider.LoadFromEnv();

        _serviceProvider = new ServiceCollection()
            .AddSingleton(secrets)
            .AddDbContext<AdribotContext>(
                optionsAction: (options) => options.UseSqlServer(secrets.Config.SqlConnectionString),
                contextLifetime: ServiceLifetime.Transient
            )
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
        await _serviceProvider.GetRequiredService<AdribotContext>().Database.MigrateAsync();
        await _serviceProvider.GetRequiredService<Bot>().StartAsync(_serviceProvider.GetRequiredService<SecretsProvider>().Config.BotToken, _serviceProvider);

        await Task.Delay(-1);
    }
}
