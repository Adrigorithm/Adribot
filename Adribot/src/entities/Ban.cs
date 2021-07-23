using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.entities
{
    [Table("bans")]
    public class Ban {
        [Key]
        [Column("banId")]
        public int BanId { get; set; }

        [Column("guildId")]
        public ulong GuildId { get; set; }

        [Column("userId")]
        public ulong UserId { get; set; }

        [Column("banExpired")]
        public DateTime BanExpired { get; set; }

        [Column("isBanned")]
        public bool IsBanned { get; set; }

        [Column("reason")]
        public string Reason { get; set; }
    }
}
