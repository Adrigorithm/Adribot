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

        public List<Tag> Tags { get; set; }

        public ulong GuildId { get; set; }
        public Guild Guild { get; set; }

        public ulong UserId { get; set; }
        public User User { get; set; }

        public int BanId { get; set; }
        public Ban Ban { get; set; }

        public int MuteId { get; set; }
        public Mute Mute { get; set; }
    }
}
