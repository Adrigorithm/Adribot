using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.data.repositories;
using Adribot.src.extensions;
using Adribot.src.services.providers;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

namespace Adribot.src.events;

public class ClientEvents
{
    private readonly DiscordClientProvider _clientProvider;
    private readonly DGuildRepository _dGuildRepository;

    public ClientEvents(DiscordClientProvider clientProvider, DGuildRepository dGuildRepository)
    {
        _clientProvider = clientProvider;
        _dGuildRepository = dGuildRepository;

        SlashCommandsExtension slashies = _clientProvider.Client.GetExtension<SlashCommandsExtension>();

        _clientProvider.Client.MessageCreated += MessageCreatedAsync;
        slashies.SlashCommandErrored += SlashCommandErrored;
        _clientProvider.Client.GuildDownloadCompleted += GuildDownloadCompletedAsync;
    }

    private async Task GuildDownloadCompletedAsync(DiscordClient sender, GuildDownloadCompletedEventArgs e)
    {
        IEnumerable<DiscordGuild> guilds = sender.Guilds.Values;
        FrozenDictionary<ulong, ulong[]> guildMembers = _dGuildRepository.GetGuildsWithMembers();

        for (var i = 0; i < guilds.Count(); i++)
        {
            DiscordGuild guildCurrent = guilds.ElementAt(i);
            var isCachedGuild = guildMembers.ContainsKey(guildCurrent.Id);

            if (!isCachedGuild)
            {
                _ = _dGuildRepository.AddDGuild(guildCurrent.ToDGuild());
                List<(ulong, string)> membersToAdd = [];

                await foreach (DiscordMember member in guildCurrent.GetAllMembersAsync())
                    membersToAdd.Add((member.Id, member.Mention));

                _dGuildRepository.AddMembersToGuild(guildCurrent.Id, membersToAdd);
            }
            else
            {
                var cachedMembers = guildMembers[guildCurrent.Id].ToHashSet();
                List<(ulong, string)> membersToAdd = [];

                foreach (KeyValuePair<ulong, DiscordMember> newMember in guildCurrent.Members)
                {
                    if (cachedMembers.Add(newMember.Key))
                        membersToAdd.Add((newMember.Key, newMember.Value.Mention));
                }

                _dGuildRepository.AddMembersToGuild(guildCurrent.Id, membersToAdd);
            }
        }
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
