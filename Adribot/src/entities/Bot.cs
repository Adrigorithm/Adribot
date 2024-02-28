using System.Threading.Tasks;
using Adribot.src.commands.fun;
using Adribot.src.commands.moderation;
using Adribot.src.commands.utilities;
using Adribot.src.data;
using Adribot.src.data.repositories;
using Adribot.src.events;
using Adribot.src.services;
using Adribot.src.services.providers;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;

namespace Adribot.src.entities;

public class Bot
{
    private readonly DiscordClient _client;

    public Bot()
    {
        var secrets = new SecretsProvider();

        _client = new(new DiscordConfiguration
        {
            Token = secrets.Config.BotToken,
            Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
        });

        ServiceProvider services = new ServiceCollection()
            .AddSingleton(secrets)
            .AddDbContext<AdribotContext>()
            .AddSingleton(new DiscordClientProvider(_client))
            .AddSingleton<DGuildRepository>()
            .AddSingleton<InfractionRepository>()
            .AddSingleton<RemindMeRepository>()
            .AddSingleton<IcsCalendarRepository>()
            .AddSingleton<DGuildRepository>()
            .AddSingleton<TagRepository>()
            .AddSingleton<ClientEvents>()
            .AddSingleton<BaseTimerService, InfractionService>()
            .AddSingleton<BaseTimerService, RemindMeSerivce>()
            .AddSingleton<BaseTimerService, IcsCalendarService>()
            .AddSingleton<BaseTimerService, StarboardService>()
            .AddSingleton<TagService>()
            .BuildServiceProvider();

        SlashCommandsExtension slashies = _client.UseSlashCommands(new SlashCommandsConfiguration()
        {
            Services = services
        });

        slashies.RegisterCommands<AdminCommands>(1023986117428658187);
        slashies.RegisterCommands<MinecraftCommands>(1023986117428658187);
        slashies.RegisterCommands<FunCommands>(1023986117428658187);
        slashies.RegisterCommands<UtilityCommands>(1023986117428658187);
        slashies.RegisterCommands<TagCommands>(1023986117428658187);
        slashies.RegisterCommands<CalendarCommands>(1023986117428658187);
    }

    public async Task StartAsync() =>
        await _client.ConnectAsync();
    public async Task StopAsync() =>
        await _client.DisconnectAsync();
}
