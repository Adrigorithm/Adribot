using System;
//using Adribot.src.config;
using Adribot.src.entities.discord;
using Adribot.src.entities.utilities;
using Adribot.src.services.providers;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data;

public class AdribotContext(SecretsProvider _secrets) : DbContext
{
    public DbSet<DGuild> DGuilds { get; set; }
    public DbSet<DMember> DMembers { get; set; }
    public DbSet<Infraction> Infractions { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<IcsCalendar> IcsCalendars { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_secrets.Config.SqlConnectionString);
        options.LogTo(Console.WriteLine);
    }
}
