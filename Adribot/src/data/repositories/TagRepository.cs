using System.Collections.Generic;
using System.Linq;
using Adribot.src.entities.utilities;
using Microsoft.EntityFrameworkCore;

namespace Adribot.src.data.repositories;

public class TagRepository(AdribotContext _botContext)
{
    public IEnumerable<Tag> GetAllTags() =>
        _botContext.Tags.Include(t => t.DMember).Include(t => t.DMember.DGuild);

    public Tag AddTag(ulong guildId, ulong memberId, Tag tag)
    {
        tag.DMember = _botContext.DMembers.Include(dm => dm.DGuild).First(dm => dm.MemberId == memberId && dm.DGuild.GuildId == guildId);
        _botContext.Add(tag);
        _botContext.SaveChanges();
        return tag;
    }

    public void UpdateTag(Tag tag)
    {
        _botContext.Update(tag);
        _botContext.SaveChanges();
    }

    public void Remove(Tag tag)
    {
        _botContext.Remove(tag);
        _botContext.SaveChanges();
    }
}
