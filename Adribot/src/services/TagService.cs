using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.data;
using Adribot.src.entities.utilities;
using DSharpPlus;

namespace Adribot.src.services;

public sealed class TagService
{
    private readonly DiscordClient _client;

    /// <summary>
    /// TagId on the individual tags will be 0 when they are added later to minimise database interactions.
    /// Do NOT depend on the ids.
    /// </summary>
    private List<Tag> _tags { get; }

    public TagService(DiscordClient client)
    {
        _client = client;

        using var database = new DataManager();
        _tags = database.GetAllInstances<Tag>().ToList();
    }

    public async Task<bool> TrySetTagAsync(Tag tag, bool shouldOverwrite = false)
    {
        Tag? oldTag = null;
        var oldTagIndex = -1;

        for (var i = 0; i < _tags.Count; i++)
        {
            if (_tags[i] == tag)
                (oldTag, oldTagIndex) = (_tags[i], i);
        }

        if (oldTag is null || shouldOverwrite)
        {
            using var database = new DataManager();

            if (oldTag is null)
            {
                await database.AddInstanceAsync(tag);
                _tags.Add(tag);
            }
            else
            {
                tag.TagId = oldTag.TagId;
                _tags[oldTagIndex] = tag;
                database.UpdateInstance(tag);
            }

            return true;
        }
        return false;
    }

    public IEnumerable<Tag> GetAllTags(ulong guildId) =>
        _tags.Where(t => t.DGuildId == guildId);


    public Tag? TryGetTag(string tagName, ulong guildId) =>
        _tags.FirstOrDefault(t => t.Name == tagName && t.DGuildId == guildId);

    public bool TryRemoveTag(string tagname, ulong guildId)
    {
        Tag? tag = TryGetTag(tagname, guildId);
        if (tag is not null)
        {
            _tags.Remove(tag);

            using var database = new DataManager();
            database.RemoveInstance(tag);

            return true;
        }
        return false;
    }
}