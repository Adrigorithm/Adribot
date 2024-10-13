using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using Adribot.Data;
using Adribot.Entities.Discord;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Data.Repositories;

public sealed class DGuildRepository : BaseRepository
{
    public DGuildRepository(IDbContextFactory<AdribotContext> botContextFactory) : base(botContextFactory) { }

    public Dictionary<ulong, (ulong channelId, string? starEmoji, int? threshold)> GetStarboards()
    {
        using AdribotContext botContext = CreateDbContext();

        return botContext.DGuilds.Where(dg => dg.StarboardChannel != null).ToDictionary(dg => dg.GuildId, dg => ((ulong)dg.StarboardChannel, dg.StarEmoji, dg.StarThreshold));
    }

    public void SetStarboard(ulong guildId, ulong channelId, string? starEmoji, int? threshold)
    {
        using AdribotContext botContext = CreateDbContext();

        DGuild guild = botContext.DGuilds.First(dg => dg.GuildId == guildId);
        guild.StarboardChannel = channelId;
        guild.StarEmoji = starEmoji;
        guild.StarThreshold = threshold;

        botContext.SaveChanges();
    }

    public FrozenDictionary<ulong, ulong[]> GetGuildsWithMembers()
    {
        using AdribotContext botContext = CreateDbContext();

        return botContext.DGuilds.Include(dg => dg.Members).ToFrozenDictionary(dg => dg.GuildId, dg => dg.Members.Select(dm => dm.MemberId).ToArray());
    }

    public void AddDGuild(DGuild dGuild)
    {
        using AdribotContext botContext = CreateDbContext();

        botContext.DGuilds.Add(dGuild);
        botContext.SaveChanges();
    }

    public DGuild GetGuild(ulong guildId)
    {
        using AdribotContext botContext = CreateDbContext();

        return botContext.DGuilds.First(dg => dg.GuildId == guildId);
    }

    public void UpdateDGuild(DGuild dGuild)
    {
        using AdribotContext botContext = CreateDbContext();

        botContext.Update(dGuild);
        botContext.SaveChanges();
    }

    public void AddMembersToGuild(ulong guildId, IEnumerable<(ulong, string)> membersToAdd)
    {
        using AdribotContext botContext = CreateDbContext();

        DGuild guild = botContext.DGuilds.First(dg => dg.GuildId == guildId);

        foreach ((ulong, string) member in membersToAdd)
        {
            guild.Members.Add(new DMember
            {
                DGuild = guild,
                MemberId = member.Item1,
                Mention = member.Item2
            });
        }

        botContext.Update(guild);
        botContext.SaveChanges();
    }
}
