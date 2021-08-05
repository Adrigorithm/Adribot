using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.entities
{
    public class GuildMember {
        public ulong GuildId { get; set; }
        public Guild Guild { get; set; }

        public ulong UserId { get; set; }
        public Member Member { get; set; }
    }
}
