using System;
using Adribot.src.entities.discord;
using Adribot.src.entities.utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data;

public class AdribotContext() : DbContext
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
        options.UseSqlServer("Server=adribot_db;Database=AdribotDB;User Id=SA;Password=eg3lLQm42y$hr%J@A%iV3fH#5zJHr^H2");
        options.LogTo(Console.WriteLine);
    }
}
