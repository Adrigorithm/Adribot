using System;
using Adribot.config;
using Adribot.entities.discord;
using Adribot.entities.utilities;
using Microsoft.EntityFrameworkCore;

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

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(_connectionString);
        options.LogTo(Console.WriteLine);
    }
}
