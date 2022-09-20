using Microsoft.EntityFrameworkCore;

internal class AdribotContext : DbContext{
    private string _connectionString;

    public DbSet<DGuild> Guilds {get; set;}
    public DbSet<DMember> Members {get; set;}
    public DbSet<Infraction> Infractions {get; set;}

    public AdribotContext(){}
    public AdribotContext(string connectionString) => _connectionString = connectionString;
    
    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=adribot;Trusted_Connection=True;");
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<DMember>().HasKey(ck => new {ck.MemberId, ck.GuildId});
    }
}