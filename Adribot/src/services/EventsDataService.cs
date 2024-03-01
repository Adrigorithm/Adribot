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

namespace Adribot.src.services;

public class EventsDataService
{
    private readonly DiscordClientProvider _clientProvider;
    private readonly DGuildRepository _dGuildRepository;

    public EventsDataService(DiscordClientProvider clientProvider, DGuildRepository dGuildRepository)
    {
        _clientProvider = clientProvider;
        _dGuildRepository = dGuildRepository;

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
}