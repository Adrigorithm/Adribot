using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.SlashCommands;

public class Bot
{
    private Secret _secret;
    private ClientEvents _clientEvents;
    private DiscordClient _client;
    public Bot() => SetupClient();

    private void SetupClient()
    {
        _secret = Task.Run(async () => await LoadConfigurationAsync()).Result;

        _client = new(new DiscordConfiguration
        {
            Token = _secret.Bot,
            Intents = DiscordIntents.All
        });

        var slashies = _client.UseSlashCommands();
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
            UseSlashCommandErrored = true
        };
        _clientEvents.Attach();
    }

    private async Task<Secret> LoadConfigurationAsync() => await JsonSerializer.DeserializeAsync<Secret>(File.OpenRead("./secret/tokens.json"));
    public async Task StartAsync() => await _client.ConnectAsync();
    public async Task StopAsync() => await _client.DisconnectAsync();
}