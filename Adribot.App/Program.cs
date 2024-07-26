using System.Threading.Tasks;
using Adribot.src.data;
using Adribot.src.data.repositories;
using Adribot.src.entities;
using Adribot.src.services;
using Adribot.src.services.providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Adribot;

internal static class Program
{
    private static IHost _host;

    public static async Task Main(string[] args)
    {
        IHostBuilder builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) => 
            {
                config.AddEnvironmentVariables();
            });
        var secrets = SecretsProvider.LoadFromEnv();

        builder.ConfigureServices(services =>
        {
            services.AddSingleton(secrets);
            services.AddDbContext<AdribotContext>(
                optionsAction: (options) => options.UseSqlServer(secrets.Config.SqlConnectionString)
            );
            services.AddSingleton<DGuildRepository>();
            services.AddSingleton<DiscordClientProvider>();
            services.AddSingleton<Bot>();
            services.AddSingleton<RemoteAccessService>();
            services.AddSingleton<InfractionRepository>();
            services.AddSingleton<RemindMeRepository>();
            services.AddSingleton<IcsCalendarRepository>();
            services.AddSingleton<TagRepository>();
            services.AddSingleton<InfractionService>();
            services.AddSingleton<RemindMeSerivce>();
            services.AddSingleton<IcsCalendarService>();
            services.AddSingleton<StarboardService>();
            services.AddSingleton<TagService>();
        });

        _host = builder.Build();
        
        await _host.RunAsync();
        await RunAsync();
    }

    private static async Task RunAsync()
    {
        Bot bot = _host.Services.GetRequiredService<Bot>();
        await bot.StartAsync(_host.Services.GetRequiredService<SecretsProvider>().Config.BotToken);
        
        await Task.Delay(-1);
    }
}
