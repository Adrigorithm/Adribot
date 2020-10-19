using System;
using System.Collections.Generic;
using System.Text;

namespace Adribot.src.entities
{
    public class Ban
    {
        private ulong _guildId, _userId;
        private DateTime _banExpired;

        public Ban(ulong guildId, ulong userId, DateTime banExpired) {
            _guildId = guildId;
            _userId = userId;
            _banExpired = banExpired;
        }

        public ulong GuildId { get => _guildId; }
        public ulong UserId { get => _userId; }
        public DateTime BanExpired { get => _banExpired; }
    }
}
