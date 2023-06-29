using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.entities.discord;
using Adribot.extensions;
using DSharpPlus;
using DSharpPlus.Entities;

namespace Adribot.data;

public class DataManager : IDisposable
{
    private readonly AdribotDb _database;

    /// <summary>
    /// Instantiates a new database connection, this should be treated as a Database context, 
    /// and should therefore be thrown away after a single unit of work is done.
    /// While it could theoretically be reused, I do suggest you reinstantiate everytime work on the database is to be done as it takes up a considerable amount of space in memory.
    /// </summary>
    /// <param name="guilds">If not all guilds should be synchronised with the stored data, they can be excluded in this IEnumerable.</param>
    public DataManager(DiscordClient client)
    {
        _database = new();

        AddGuildsAsync(client);
    }

    private async void AddGuildsAsync(DiscordClient client)
    {
        IEnumerable<DiscordGuild> guilds = client.Guilds.Values;

        for (int j = 0; j < guilds.Count(); j++)
        {
            DGuild? selectedGuild = _database.DGuilds.FirstOrDefault(g => g.DGuildId == guilds.ElementAt(j).Id);
            if (selectedGuild is null)
            {
                _database.DGuilds.Add(new DGuild
                {
                    DGuildId = guilds.ElementAt(j).Id
                });
                _database.DMembers.AddRange((await guilds.ElementAt(j).GetAllMembersAsync()).ToDMembers());
            } else
                _database.DMembers.AddRange(selectedGuild.GetMembersDifference((await (await client.GetGuildAsync(selectedGuild.DGuildId)).GetAllMembersAsync()).ToDMembers()));
        }

        _database.SaveChanges();
    }

    public async void Dispose() =>
        await _database.DisposeAsync();
}
