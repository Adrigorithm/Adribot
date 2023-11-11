using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.entities.discord;
using Adribot.src.entities.utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Adribot.src.data;

public class DataManager : IDisposable
{
    private readonly AdribotDb _database;
    private string _insertionTableName;

    /// <summary>
    /// Instantiates a new database connection, this should be treated as a Database context, 
    /// and should therefore be thrown away after a single unit of work is done.
    /// While it could theoretically be reused, I do suggest you reinstantiate everytime work on the database is to be done as it takes up a considerable amount of space in memory.
    /// </summary>
    /// <param name="client">The client should be provided to allow real-time tracking of entities</param>
    public DataManager() =>
        _database = new();

    public void UpdateInstance<T>(T entity) where T : IDataStructure =>
        _database.Update(entity);

    public void RemoveInstance<T>(T entity) where T : IDataStructure =>
        _database.Remove(entity);

    public async Task AddInstanceAsync<T>(T entity, bool enableIdInsertion = false) where T : IDataStructure
    {
        if (enableIdInsertion)
            _insertionTableName = typeof(T).Name;

        await _database.AddAsync(entity);
    }

    public async Task AddAllInstancesAsync<T>(IEnumerable<T> entityList, bool enableIdInsertion = false) where T : IDataStructure
    {
        for (var i = 0; i < entityList.Count(); i++)
        {
            if (i == 0 && enableIdInsertion)
                _insertionTableName = typeof(T).Name;

            await AddInstanceAsync(entityList.ElementAt(i));
        }
    }

    public List<Infraction> GetInfractionsToOldNotExpired() =>
        _database.Infractions.OrderByDescending(i => i.EndDate).Where(i => !i.IsExpired).ToList();

    public List<IcsCalendar> GetIcsCalendarsNotExpired(DateTimeOffset now) =>
        _database.IcsCalendars.Where(c => c.Events.OrderBy(e => e.Start).Last().End > now).ToList();

    public List<string> GetIcsCalendarNames() =>
        _database.IcsCalendars.Select(c => c.Name).ToList();

    public List<Reminder> GetRemindersToOld() =>
        _database.Reminders.OrderByDescending(r => r.EndDate).ToList();

    public List<DGuild> GetDGuildsStarboardNotNull() =>
        _database.DGuilds.Where(dg => dg.StarboardChannel != null).ToList();

    public IEnumerable<T> GetAllInstances<T>() where T : class, IDataStructure =>
        _database.Set<T>();

    public void Dispose()
    {
        using IDbContextTransaction transaction = _database.Database.BeginTransaction();

        if (_insertionTableName is not null)
            _database.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo.{_insertionTableName}s ON");

        _database.SaveChanges();

        if (_insertionTableName is not null)
            _database.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo.{_insertionTableName}s OFF");

        transaction.Commit();

        _database.Dispose();
    }
}
