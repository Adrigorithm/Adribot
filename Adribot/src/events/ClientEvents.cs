using System;
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
            _client.GuildDownloadCompleted += GuildDownloadCompletedAsync;
    }

    private async Task GuildDownloadCompletedAsync(DiscordClient sender, GuildDownloadCompletedEventArgs e)
    {
        using DataManager database = new(sender);
        await database.AddGuildsAsync();
    }

    private Task SlashCommandErrored(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
    {
        Console.WriteLine($"{e.Context.CommandName}\n{e.Exception.Message}");

        return Task.CompletedTask;
    }

    private async Task MessageCreatedAsync(DiscordClient client, MessageCreateEventArgs args)
    {
        var member = args.Author as DiscordMember;
        bool pingedAdmin = false;
        int counter = 0;

        while (counter < args.MentionedUsers.Count && !pingedAdmin)
        {
            pingedAdmin = (await args.Guild.GetMemberAsync(args.MentionedUsers[counter].Id))?.Permissions.HasPermission(Permissions.Administrator) ?? false;
            counter++;
        }

        if (member is not null &&
            !args.Channel.IsPrivate &&
            !member.IsBot &&
            !member.Permissions.HasPermission(Permissions.Administrator) &&
            pingedAdmin)
        {
            await args.Message.CreateReactionAsync(DiscordEmoji.FromUnicode("💢"));
        }
    }
}
