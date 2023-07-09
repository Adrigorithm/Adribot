using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.entities.discord;
using Adribot.extensions;
using Adribot.src.data;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.data;

public class DataManager : IDisposable
{
    private readonly AdribotDb _database;
    private readonly DiscordClient _client;

    /// <summary>
    /// Instantiates a new database connection, this should be treated as a Database context, 
    /// and should therefore be thrown away after a single unit of work is done.
    /// While it could theoretically be reused, I do suggest you reinstantiate everytime work on the database is to be done as it takes up a considerable amount of space in memory.
    /// </summary>
    /// <param name="client">The client should be provided to allow real-time tracking of entities</param>
    public DataManager(DiscordClient client)
    {
        _database = new();
        _client = client;
    }

    public async Task AddGuildsAsync()
    {
        IEnumerable<DiscordGuild> guilds = _client.Guilds.Values;

        for (int j = 0; j < guilds.Count(); j++)
        {
            DGuild? selectedGuild = await _database.DGuilds.FirstOrDefaultAsync(g => g.DGuildId == guilds.ElementAt(j).Id);
            if (selectedGuild is null)
            {
                _database.DGuilds.Add(new DGuild
                {
                    DGuildId = guilds.ElementAt(j).Id
                });
                await _database.DMembers.AddRangeAsync((await guilds.ElementAt(j).GetAllMembersAsync()).ToDMembers());
            } else
                await _database.DMembers.AddRangeAsync(selectedGuild.GetMembersDifference((await (await _client.GetGuildAsync(selectedGuild.DGuildId)).GetAllMembersAsync()).ToDMembers()));
        }

        await _database.SaveChangesAsync();
    }

    public void UpdateInstance<T>(T entity) where T : IDataStructure
    {
        _database.Update(entity);
        _database.SaveChanges();
    }

    public async Task AddInstanceAsync<T>(T entity) where T : IDataStructure
    {
        await _database.AddAsync(entity);
        await _database.SaveChangesAsync();
    }

    public List<Infraction> GetInfractions() =>
        _database.Infractions.OrderByDescending(i => i.EndDate).Where(i => !i.IsExpired).ToList();

    public async void Dispose() =>
        await _database.DisposeAsync();
}
