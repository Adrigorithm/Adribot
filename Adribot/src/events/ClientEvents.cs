using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.config;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Exceptions;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

class ClientEvents
{
    private DiscordClient _client;
    private Config _config;

    public bool UseMessageCreated;
    public bool UseGuildDownloadCompleted;

    // Slashies
    public bool UseSlashCommandErrored;

    public ClientEvents(DiscordClient client, Config config) => (_client, _config) = (client, config);
    
    public void Attach()
    {
        var slashies = _client.GetExtension<SlashCommandsExtension>();

        if (UseMessageCreated)
            _client.MessageCreated += MessageCreatedAsync;
        if (UseSlashCommandErrored)
            slashies.SlashCommandErrored += SlashCommandErroredAsync;
        if (UseGuildDownloadCompleted)
            _client.GuildDownloadCompleted += GuildDownloadCompletedAsync;
    }

    private Task GuildDownloadCompletedAsync(DiscordClient sender, GuildDownloadCompletedEventArgs e){
        InfractionService.Init(_client, _config.SQLConnectionString);
        return Task.CompletedTask;
    }

    private Task SlashCommandErroredAsync(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
    {
        Console.WriteLine($"{e.Context.CommandName}\n{e.Exception.Message}");

        return Task.CompletedTask;
    }

    private async Task MessageCreatedAsync(DiscordClient client, MessageCreateEventArgs args)
    {
        if (args.Guild != null && !args.Author.IsBot && args.MentionedUsers.Count > 0 && !((DiscordMember)args.Author).Permissions.HasPermission(Permissions.Administrator))
        {
            foreach (var user in args.MentionedUsers)
            {
                if (((DiscordMember)user).Permissions.HasPermission(Permissions.Administrator) || user.Id == 135081249017430016)
                    try
                    {
                        await args.Message.CreateReactionAsync(DiscordEmoji.FromName(client, ":killercat:"));
                    }
                    catch (NotFoundException)
                    {
                        await args.Message.CreateReactionAsync(DiscordEmoji.FromName(client, ":knife:"));
                    }
            }
        }
    }
}