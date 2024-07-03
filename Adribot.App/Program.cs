using System;
using System.Threading.Tasks;
using Adribot.src.entities;
using Adribot.src.services.providers;
using Microsoft.Extensions.DependencyInjection;

namespace Adribot;

internal static class Program
{
    private static IServiceProvider _serviceProvider;

    private static async Task Main(string[] args)
    {
        var secrets = new SecretsProvider();

        _serviceProvider = new ServiceCollection()
            .AddSingleton(secrets)
            .BuildServiceProvider();

        Bot bot = new();
        //await bot.StartAsync();

        await Task.Delay(-1);
    }
}
