using System.Text;
using Adribot.Entities.Discord;
using Adribot.Entities.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Data;

public class AdribotContext(DbContextOptions<AdribotContext> options) : DbContext(options)
{
    public DbSet<DGuild> DGuilds { get; set; }
    public DbSet<DMember> DMembers { get; set; }
    public DbSet<Infraction> Infractions { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<IcsCalendar> IcsCalendars { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder
            .Entity<DGuild>()
            .Property(dg => dg.StarEmotes)
            .HasConversion(
                el =>
                {
                    var emotesString = new StringBuilder();
                    
                    el.ForEach(e => emotesString.AppendLine(e.Name));
                    
                    return emotesString.ToString();
                }, el =>
                {
                    
                })
    }
}
