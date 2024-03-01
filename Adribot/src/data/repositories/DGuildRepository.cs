using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using Adribot.src.entities.discord;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data.repositories;

public class DGuildRepository(AdribotContext _botContext)
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

    public FrozenDictionary<ulong, ulong[]> GetGuildsWithMembers() =>
        _botContext.DGuilds.Include(dg => dg.Members).ToFrozenDictionary(dg => dg.GuildId, dg => dg.Members.Select(dm => dm.MemberId).ToArray());

    public void AddDGuild(DGuild dGuild)
    {
        _botContext.DGuilds.Add(dGuild);
        _botContext.SaveChanges();
    }

    public DGuild GetGuild(ulong guildId) =>
        _botContext.DGuilds.First(dg => dg.GuildId == guildId);

    public void UpdateDGuild(DGuild dGuild)
    {
        _botContext.Update(dGuild);
        _botContext.SaveChanges();
    }

    public void AddMembersToGuild(ulong guildId, IEnumerable<(ulong, string)> membersToAdd)
    {
        DGuild guild = _botContext.DGuilds.First(dg => dg.GuildId == guildId);

        foreach ((ulong, string) member in membersToAdd)
        {
            guild.Members.Add(new DMember
            {
                DGuild = guild,
                MemberId = member.Item1,
                Mention = member.Item2
            });
        }

        _botContext.Update(guild);
        _botContext.SaveChanges();
    }
}
