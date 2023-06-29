using System.Threading.Tasks;
using Adribot.commands.fun;
using Adribot.commands.moderation;
using Adribot.commands.utilities;
using Adribot.config;
using Adribot.events;
using Adribot.services;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;

namespace Adribot.entities;

public class Bot
{
    private ClientEvents? _clientEvents;
    private readonly DiscordClient _client;

    public Bot()
    {
        Task.Run(async () => await Config.LoadConfigAsync()).Wait();

        _client = new(new DiscordConfiguration
        {
            Token = Config.Configuration.BotToken,
            Intents = DiscordIntents.AllUnprivileged | DiscordIntents.MessageContents
        });

        InfractionService infractionService = new(client: _client);

        ServiceProvider services = new ServiceCollection()
            .AddSingleton(infractionService)
            .BuildServiceProvider();

        SlashCommandsExtension slashies = _client.UseSlashCommands(new SlashCommandsConfiguration()
        {
            Services = services
        });

        slashies.RegisterCommands<AdminCommands>(1023986117428658187);
        slashies.RegisterCommands<MinecraftCommands>();
        slashies.RegisterCommands<FunCommands>(1023986117428658187);
        slashies.RegisterCommands<UtilityCommands>(1023986117428658187);

        AttachEvents(services);
    }


    private void AttachEvents(ServiceProvider services)
    {
        _clientEvents = new(_client)
        {
            UseMessageCreated = true,
            UseSlashCommandErrored = true,
            UseGuildDownloadCompleted = true
        };
        _clientEvents.Attach();
    }

    public async Task StartAsync() => await _client.ConnectAsync();
    public async Task StopAsync() => await _client.DisconnectAsync();
}
