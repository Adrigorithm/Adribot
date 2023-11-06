using System.Threading.Tasks;
using Adribot.src.commands.fun;
using Adribot.src.commands.moderation;
using Adribot.src.commands.utilities;
using Adribot.src.config;
using Adribot.src.events;
using Adribot.src.services;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;

namespace Adribot.src.entities;

public class Bot
{
    private ClientEvents? _clientEvents;
    private readonly DiscordClient _client;

    public Bot()
    {
        Task.Run(Config.LoadConfigAsync).Wait();

        _client = new(new DiscordConfiguration
        {
            Token = Config.Configuration.BotToken,
            Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
        });

        InfractionService infractionService = new(_client);
        TagService tagService = new(_client);
        RemindMeSerivce remindMeSerivce = new(_client);
        DaySchemeService daySchemeService = new(_client);

        ServiceProvider services = new ServiceCollection()
            .AddSingleton(infractionService)
            .AddSingleton(tagService)
            .AddSingleton(remindMeSerivce)
            .AddSingleton(daySchemeService)
            .BuildServiceProvider();

        SlashCommandsExtension slashies = _client.UseSlashCommands(new SlashCommandsConfiguration()
        {
            Services = services
        });

        slashies.RegisterCommands<AdminCommands>(1023986117428658187);
        slashies.RegisterCommands<MinecraftCommands>();
        slashies.RegisterCommands<FunCommands>(1023986117428658187);
        slashies.RegisterCommands<UtilityCommands>(1023986117428658187);

        AttachEvents();
    }


    private void AttachEvents()
    {
        _clientEvents = new(_client)
        {
            UseMessageCreated = true,
            UseSlashCommandErrored = true,
            UseGuildDownloadCompleted = true,
        };
        _clientEvents.Attach();
    }

    public async Task StartAsync() => await _client.ConnectAsync();
    public async Task StopAsync() => await _client.DisconnectAsync();
}
