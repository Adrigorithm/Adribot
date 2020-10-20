using System;
using System.Collections.Generic;
using System.Text;

namespace Adribot.src.entities
{
    public class Ban
    {
        public int BanId { get; }
        public ulong GuildId { get; }
        public ulong UserId { get; }
        public DateTime BanExpired { get; }

        public Ban(int banId, ulong guildId, ulong userId, DateTime banExpired) =>
            (BanId, GuildId, UserId, BanExpired) = (banId, guildId, userId, banExpired);
    }
}
