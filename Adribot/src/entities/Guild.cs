using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.entities
{
    public class Guild
    {
        [Key]
        public ulong GuildId { get; set; }

        public List<GuildMember> GuildMembers { get; set; }
    }
}
