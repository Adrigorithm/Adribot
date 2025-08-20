using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using Adribot.Entities.Discord;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Data.Repositories;

public sealed class DGuildRepository : BaseRepository
{
    public DGuildRepository(IDbContextFactory<AdribotContext> botContextFactory) : base(botContextFactory) { }

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
                DGuild = guild, MemberId = member.Item1, Mention = member.Item2
            });
        }

        botContext.Update(guild);
        botContext.SaveChanges();
    }
}
