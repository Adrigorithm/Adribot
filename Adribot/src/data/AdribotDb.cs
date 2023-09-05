using Adribot.config;
using Adribot.entities.discord;
using Adribot.entities.utilities;
using Adribot.src.entities.utilities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Adribot.data;

public class AdribotDb : DbContext
{
    private readonly string _connectionString;

    /// <summary>
    /// Before creating an instance of this class the static configuration class should be instantiated.
    /// This means Config.LoadConfigAsync() should've been called before successfully.
    /// </summary>

    public AdribotDb() =>
        _connectionString = Config.Configuration.SqlConnectionString;
    public DbSet<DGuild> DGuilds { get; set; }
    public DbSet<DMember> DMembers { get; set; }
    public DbSet<Infraction> Infractions { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<IcsCalendar> IcsCalendars { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_connectionString);
        options.LogTo(Console.WriteLine);
        options.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DMember>()
            .Property(e => e.DGuildId)
            .ValueGeneratedNever();

        modelBuilder.Entity<DMember>()
            .Property(e => e.DMemberId)
            .ValueGeneratedNever();
    }
}
