using System;
using System.Threading.Tasks;
using Adribot.src.data;
using Adribot.src.data.repositories;
using Adribot.src.entities;
using Adribot.src.services;
using Adribot.src.services.providers;
using Microsoft.Extensions.DependencyInjection;

namespace Adribot;

internal static class Program
{
    private static IServiceProvider _serviceProvider;

    private static async Task Main(string[] args)
    {
        var secrets = SecretsProvider.LoadFromEnv();

        _serviceProvider = new ServiceCollection()
            .AddSingleton(secrets)
            .AddDbContext<AdribotContext>()
            .AddSingleton<DGuildRepository>()
            .AddSingleton<DiscordClientProvider>()
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
        Bot bot = new();
        //await bot.StartAsync();
        await Task.Delay(-1);
    }
}
