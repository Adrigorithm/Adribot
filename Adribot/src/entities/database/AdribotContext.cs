using Microsoft.EntityFrameworkCore;

class AdribotContext : DbContext{
    private string _connectionString;

    public DbSet<DGuild> Guilds {get; set;}
    public DbSet<DUser> Users {get; set;}
    public DbSet<Infraction> infractions {get; set;}

    public AdribotContext(string connectionString) => _connectionString = connectionString;
    
    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(_connectionString);
}