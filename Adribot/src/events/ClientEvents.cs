using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.data;
using Adribot.src.entities.discord;
using Adribot.src.extensions;
using Adribot.src.helpers;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

namespace Adribot.src.events;

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

        using (var database = new DataManager())
        {
            IEnumerable<DiscordGuild> guilds = sender.Guilds.Values;
            IEnumerable<DGuild> cachedGuilds = database.GetAllInstances<DGuild>();

            List<DGuild> guildsToAdd = new();

            for (var i = 0; i < guilds.Count(); i++)
            {
                DiscordGuild guildCurrent = guilds.ElementAt(i);
                DGuild? selectedGuild = cachedGuilds.Any() ? cachedGuilds.FirstOrDefault(g => g.DGuildId == guildCurrent.Id) : null;
                if (selectedGuild is null)
                {
                    guildsToAdd.Add(await guildCurrent.ToDGuildAsync(false));
                    membersToAdd.AddRange(await guildCurrent.GetAllMembersAsync().ToDMembersAsync(guildCurrent.Id));
                }
                else
                {
                    foreach (DMember member in selectedGuild.GetMembersDifference(await guildCurrent.GetAllMembersAsync().ToDMembersAsync(guildCurrent.Id)))
                    {
                        member.DGuildId = selectedGuild.DGuildId;
                        membersToAdd.Add(member);
                    }
                }
            }

            FakeExtensions.PrintFormat(guildsToAdd);
            await database.AddAllInstancesAsync(guildsToAdd, true);
        }

        using var databaseMembers = new DataManager();
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
        var pingedAdmin = false;
        var counter = 0;

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
