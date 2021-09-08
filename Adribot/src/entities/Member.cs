using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.entities
{
    public class Member {
        public int MemberId { get; set; }

        [Required]
        public ulong GuildId { get; set; }
        public Guild Guild { get; set; }

        [Required]
        public ulong UserId { get; set; }
        public User User { get; set; }

        public List<Tag> Tags { get; set; }
        public List<Ban> Ban { get; set; }
        public List<Mute> Mute { get; set; }
    }
}
