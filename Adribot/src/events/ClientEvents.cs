using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.data;
using Adribot.entities.discord;
using Adribot.extensions;
using Adribot.services;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

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
        List<DMember> membersToAdd = new();

        using (var database = new DataManager(sender))
        {
            IEnumerable<DiscordGuild> guilds = sender.Guilds.Values;
            IEnumerable<DGuild> cachedGuilds = database.GetAllInstances<DGuild>();

            List<DGuild> guildsToAdd = new();

            for (int i = 0; i < guilds.Count(); i++)
            {
                DGuild? selectedGuild = cachedGuilds.FirstOrDefault(g => g.DGuildId == guilds.ElementAt(i).Id);
                if (selectedGuild is null)
                {
                    guildsToAdd.Add(await guilds.ElementAt(i).ToDGuildAsync(false));
                    membersToAdd.AddRange((await guilds.ElementAt(i).GetAllMembersAsync()).ToDMembers());
                }
                else
                {
                    foreach (DMember member in selectedGuild.GetMembersDifference((await guilds.ElementAt(i).GetAllMembersAsync()).ToDMembers()))
                    {
                        member.DGuildId = selectedGuild.DGuildId;
                        membersToAdd.Add(member);
                    }
                }
            }

            await database.AddAllInstancesAsync(guildsToAdd, true);
        }

        using var databaseMembers = new DataManager(sender);
        await databaseMembers.AddAllInstancesAsync(membersToAdd);
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
