using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.config;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.EntityFrameworkCore;

public class Bot
{
    private Config _config = new();
    private ClientEvents _clientEvents;
    private DiscordClient _client;

    public Bot() => SetupClient();

    private void SetupClient()
    {
        Task.Run(async () => await _config.LoadConfigAsync()).Wait();

        _client = new(new DiscordConfiguration
        {
            Token = _config.BotToken,
            Intents = DiscordIntents.All,
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
        _clientEvents = new(_client, _config)
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