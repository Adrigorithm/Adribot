using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.entities.discord;
using Adribot.extensions;
using Adribot.src.data;
using DSharpPlus;
using DSharpPlus.Entities;

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
        IEnumerable<DGuild> cachedGuilds = GetDGuilds();

        for (int i = 0; i < guilds.Count(); i++)
        {
            DGuild? selectedGuild = cachedGuilds.FirstOrDefault(g => g.DGuildId == guilds.ElementAt(i).Id);
            if (selectedGuild is null)
                await AddInstanceAsync(await guilds.ElementAt(i).ToDGuildAsync());
            else
                selectedGuild.Members.AddRange(selectedGuild.GetMembersDifference((await guilds.ElementAt(i).GetAllMembersAsync()).ToDMembers()));
        }
    }

    public void UpdateInstance<T>(T entity) where T : IDataStructure =>
        _database.Update(entity);

    public void RemoveInstance<T>(T entity) where T : IDataStructure =>
        _database.Remove(entity);

    public async Task AddInstanceAsync<T>(T entity) where T : IDataStructure =>
        await _database.AddAsync(entity);

    public List<Infraction> GetInfractionsToOldNotExpired(bool orderByDesc = true) =>
        orderByDesc ? _database.Infractions.OrderByDescending(i => i.EndDate).Where(i => !i.IsExpired).ToList()
                    : _database.Infractions.Where(i => !i.IsExpired).ToList();

    public IEnumerable<T> GetAllInstances<T>() where T : class, IDataStructure =>
        _database.Set<T>();

    public List<DGuild> GetDGuilds() =>
        _database.DGuilds.ToList();

    public void Dispose()
    {
        _database.SaveChanges();
        _database.Dispose();
    }
}
