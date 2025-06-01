using System.Collections.Generic;
using System.Linq;
using Adribot.Entities.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Data.Repositories;

public sealed class TagRepository(IDbContextFactory<AdribotContext> botContextFactory)
    : BaseRepository(botContextFactory)
{
    public IEnumerable<Tag> GetAllTags()
    {
        using AdribotContext botContext = CreateDbContext();

        return botContext.Tags.Include(t => t.DMember).Include(t => t.DMember.DGuild).ToList();
    }

    public Tag AddTag(ulong guildId, ulong memberId, Tag tag)
    {
        using AdribotContext botContext = CreateDbContext();

        tag.DMember = botContext.DMembers.Include(dm => dm.DGuild).First(dm => dm.MemberId == memberId && dm.DGuild.GuildId == guildId);
        botContext.Add(tag);
        botContext.SaveChanges();
        return tag;
    }

    public void UpdateTag(Tag tag)
    {
        using AdribotContext botContext = CreateDbContext();

        botContext.Update(tag);
        botContext.SaveChanges();
    }

    public void Remove(Tag tag)
    {
        using AdribotContext botContext = CreateDbContext();

        botContext.Remove(tag);
        botContext.SaveChanges();
    }
}
