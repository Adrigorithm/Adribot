using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.entities
{
    public class Member
    {
        [Key]
        public ulong UserId { get; set; }

        public List<Ban> Bans { get; set; }
        public List<GuildMember> GuildMembers { get; set; }

        public ulong GuildId { get; set; }
        public Guild Guild { get; set; }
    }
}
