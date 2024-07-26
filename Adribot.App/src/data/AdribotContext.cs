using Adribot.src.entities.discord;
using Adribot.src.entities.utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data;

public class AdribotContext : DbContext
{
    public AdribotContext(DbContextOptions<AdribotContext> options) : base(options)
    {

    }

    public DbSet<DGuild> DGuilds { get; set; }
    public DbSet<DMember> DMembers { get; set; }
    public DbSet<Infraction> Infractions { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<IcsCalendar> IcsCalendars { get; set; }
    public DbSet<Event> Events { get; set; }
}
