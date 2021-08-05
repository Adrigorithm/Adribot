using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.entities.source
{
    class DBController : DbContext
    {
        public DbSet<Ban> Bans { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=AdribotDB;Integrated Security=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            #region MANY-TO-MANY -> Guild, Member
            modelBuilder.Entity<GuildMember>().HasKey("GuildId", "UserId");

            modelBuilder.Entity<GuildMember>()
                .HasOne(x => x.Guild)
                .WithMany(x => x.GuildMembers)
                .HasForeignKey(x => x.GuildId);

            modelBuilder.Entity<GuildMember>()
                .HasOne(x => x.Member)
                .WithMany(x => x.GuildMembers)
                .HasForeignKey(x => x.UserId);
            #endregion
        }
    }
}
