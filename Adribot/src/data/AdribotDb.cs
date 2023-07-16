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
        _connectionString = "Server=DESKTOP-QD7N10L\\SQL_2022;Database=AdribotDB;Encrypt=false;User Id=SA;Password=QB3F/kq=R5^m*Ccj"; //Config.Configuration.SqlConnectionString;
    public DbSet<DGuild> DGuilds { get; set; }
    public DbSet<DMember> DMembers { get; set; }
    public DbSet<Infraction> Infractions { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_connectionString);
        options.LogTo(Console.WriteLine);
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
