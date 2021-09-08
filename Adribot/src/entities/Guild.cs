using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Adribot.src.entities
{
    public class Guild
    {
        public ulong GuildId { get; set; }

        public List<Member> Members { get; set; }
    }
}
