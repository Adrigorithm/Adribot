using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Adribot.config;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;

public class Bot
{
    private Config _config;
    private ClientEvents _clientEvents;
    private DiscordClient _client;
    private AdribotContext _adribotContext;
    public Bot() => SetupClient();

    private void SetupClient()
    {
        Task.Run(async () => await _config.LoadConfigAsync()).Wait();
        _adribotContext = new(_config.SQLConnectionString);

        _client = new(new DiscordConfiguration
        {
            Token = _config.BotToken,
            Intents = DiscordIntents.All,
        });

        var slashies = _client.UseSlashCommands(new SlashCommandsConfiguration{
            Services = new ServiceCollection().AddSingleton<InfractionService>().BuildServiceProvider()
        });
        // Remove GID to make global (Production)
        slashies.RegisterCommands<AdminCommands>(574341132826312736);
        slashies.RegisterCommands<MinecraftCommands>(574341132826312736);
        slashies.RegisterCommands<AdminCommands>(357597633566605313);
        slashies.RegisterCommands<MinecraftCommands>(357597633566605313);

        AttachEvents();
    }

    private void AttachEvents()
    {
        _clientEvents = new(_client){
            UseMessageCreated = true,
            UseGuildMemberUpdated = true,
            UseSlashCommandErrored = true,
            UseUserUpdated = true
        };
        _clientEvents.Attach();
    }

    public async Task StartAsync() => await _client.ConnectAsync();
    public async Task StopAsync() => await _client.DisconnectAsync();
}