using System.Collections.Generic;
using System.Linq;
using Adribot.src.entities.utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data.repositories;

public class TagRepository : BaseRepository
{
    public TagRepository(IDbContextFactory<AdribotContext> _botContextFactory) : base(_botContextFactory) {}

    public IEnumerable<Tag> GetAllTags()
    {
        using AdribotContext _botContext = CreateDbContext();

        return _botContext.Tags.Include(t => t.DMember).Include(t => t.DMember.DGuild);
    }

    public Tag AddTag(ulong guildId, ulong memberId, Tag tag)
    {
        using AdribotContext _botContext = CreateDbContext();

        tag.DMember = _botContext.DMembers.Include(dm => dm.DGuild).First(dm => dm.MemberId == memberId && dm.DGuild.GuildId == guildId);
        _botContext.Add(tag);
        _botContext.SaveChanges();
        return tag;
    }

    public void UpdateTag(Tag tag)
    {
        using AdribotContext _botContext = CreateDbContext();

        _botContext.Update(tag);
        _botContext.SaveChanges();
    }

    public void Remove(Tag tag)
    {
        using AdribotContext _botContext = CreateDbContext();

        _botContext.Remove(tag);
        _botContext.SaveChanges();
    }
}
