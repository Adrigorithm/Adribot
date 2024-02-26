using System.Collections.Generic;
using System.Linq;
using Adribot.src.entities.discord;

namespace Adribot.src.data.repositories;

public class StarboardRepository(AdribotContext _botContext)
{
    public Dictionary<ulong, (ulong channelId, string? starEmoji, int? threshold)> GetStarboards() =>
        _botContext.DGuilds.Where(dg => dg.StarboardChannel != null).ToDictionary(dg => dg.GuildId, dg => ((ulong)dg.StarboardChannel, dg.StarEmoji, dg.StarThreshold));

    public void SetStarboard(ulong guildId, ulong channelId, string? starEmoji, int? threshold)
    {
        DGuild guild = _botContext.DGuilds.First(dg => dg.GuildId == guildId);
        guild.StarboardChannel = channelId;
        guild.StarEmoji = starEmoji;
        guild.StarThreshold = threshold;

        _botContext.SaveChanges();
    }
}