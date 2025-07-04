using Adribot.Entities.Discord;
using Adribot.Entities.fun;
using Adribot.Entities.Fun.Recipe;
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

    public DbSet<Starboard> Starboards { get; set; }
    public DbSet<MessageLink> MessageLinks { get; set; }

    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<WireConfig> WireConfigs { get; set; }
}
