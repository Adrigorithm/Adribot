using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adribot.src.entities
{
    public class User
    {
        public ulong UserId { get; set; }

        public List<Member> Members { get; set; }
    }
}
