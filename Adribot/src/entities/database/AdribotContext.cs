using Microsoft.EntityFrameworkCore;

internal class AdribotContext : DbContext{
    private string _connectionString;

    public DbSet<DGuild> Guilds {get; set;}
    public DbSet<DMember> Members {get; set;}
    public DbSet<Infraction> Infractions {get; set;}

    public AdribotContext(string connectionString) => _connectionString = connectionString;
    
    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(_connectionString);
}