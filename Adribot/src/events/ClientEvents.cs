using System;
using System.Linq;
using System.Threading.Tasks;
using Adribot.data;
using Adribot.services;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;
using Microsoft.Extensions.DependencyInjection;

namespace Adribot.events;

public class ClientEvents
{
    private readonly DiscordClient _client;
    public IServiceProvider Services { internal get; set; } = new ServiceCollection().BuildServiceProvider(validateScopes: true);
    public InfractionService InfractionService { private get; set; }
    
    public bool UseMessageCreated;
    public bool UseGuildDownloadCompleted;

    // Slashies
    public bool UseSlashCommandErrored;

    public ClientEvents(DiscordClient client) =>
        _client = client;

    public void Attach()
    {
        SlashCommandsExtension slashies = _client.GetExtension<SlashCommandsExtension>();

        if (UseMessageCreated)
            _client.MessageCreated += MessageCreatedAsync;
        if (UseSlashCommandErrored)
            slashies.SlashCommandErrored += SlashCommandErrored;
        if (UseGuildDownloadCompleted)
            _client.GuildDownloadCompleted += GuildDownloadCompleted;
    }

    private Task GuildDownloadCompleted(DiscordClient sender, GuildDownloadCompletedEventArgs e)
    {
        _ = Task.Run( () => new DataManager(sender));
        return Task.CompletedTask;
    }

    private Task SlashCommandErrored(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
    {
        Console.WriteLine($"{e.Context.CommandName}\n{e.Exception.Message}");

        return Task.CompletedTask;
    }

    private async Task MessageCreatedAsync(DiscordClient client, MessageCreateEventArgs args)
    {
        if (!args.Channel.IsPrivate &&
            !args.Author.IsBot &&
            !((DiscordMember)args.Author).Permissions.HasPermission(Permissions.Administrator) &&
            args.MentionedUsers.Any(user => ((DiscordMember)user).Permissions.HasPermission(Permissions.Administrator)))
        {
            await args.Message.CreateReactionAsync(DiscordEmoji.FromUnicode("💢"));
        }
    }
}
