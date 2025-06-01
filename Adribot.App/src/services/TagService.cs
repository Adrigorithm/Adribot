using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Adribot.Data.Repositories;
using Adribot.Entities.Utilities;
using Adribot.Helpers;

namespace Adribot.Services;

public sealed class TagService(TagRepository tagRepository)
{
    private Dictionary<ulong, Dictionary<string, Tag>> Tags { get; } = [];

    // TODO: Please optimise this
    public void SetTag(ulong guildId, ulong memberId, Tag tag)
    {
        EnsureTagsLoaded();

        if (Tags.TryGetValue(guildId, out Dictionary<string, Tag>? tags))
        {
            if (tags.TryGetValue(tag.Name, out Tag? cachedTag))
            {
                cachedTag.Overwrite(tag);
                tagRepository.UpdateTag(cachedTag);
                Tags[guildId][cachedTag.Name] = cachedTag;

                return;
            }

            Tag addedTag = tagRepository.AddTag(guildId, memberId, tag);
            Tags[guildId][addedTag.Name] = addedTag;

            return;
        }

        Tag newTag = tagRepository.AddTag(guildId, memberId, tag);

        Tags[guildId] = [];
        Tags[guildId][newTag.Name] = newTag;
    }

    public ImmutableArray<Tag> GetAllTags(ulong guildId)
    {
        EnsureTagsLoaded();

        return Tags.ContainsKey(guildId)
            ? Tags[guildId].Values.ToImmutableArray()
            : [];
    }

    public Tag? TryGetTag(string tagName, ulong guildId)
    {
        EnsureTagsLoaded();

        return Tags.ContainsKey(guildId)
            ? Tags[guildId].GetValueOrDefault(tagName)
            : null;
    }

    public bool TryRemoveTag(string tagname, ulong guildId)
    {
        EnsureTagsLoaded();

        if (string.IsNullOrWhiteSpace(tagname))
            return false;

        Tag? tag = TryGetTag(tagname, guildId);

        if (tag is not null)
        {
            tagRepository.Remove(tag);
            Tags[guildId].Remove(tagname);

            return true;
        }

        return false;
    }

    public (Tag?, string?) CreateTempTag(ulong guildId, ulong memberId, string tagName, string tagContent,
        DateTimeOffset createdAt, bool allowOverride)
    {
        EnsureTagsLoaded();

        return FakeExtensions.AreAllNullOrWhiteSpace(tagName, tagContent)
            ? (null, $"The {nameof(tagName)} and {nameof(tagContent)} cannot be empty.")
            : !Tags.ContainsKey(guildId) || !Tags[guildId].TryGetValue(tagName, out Tag tag) || (allowOverride && tag.DMember.MemberId == memberId)
                ? (new Tag
                {
                    Content = tagContent,
                    Date = createdAt,
                    Name = tagName
                }, null)
                : (null, "Tag name already taken!");
    }

    private void EnsureTagsLoaded()
    {
        if (Tags.Any())
            return;

        tagRepository.GetAllTags().ToList().ForEach(t =>
        {
            if (!Tags.ContainsKey(t.DMember.DGuild.GuildId))
                Tags[t.DMember.DGuild.GuildId] = [];

            Tags[t.DMember.DGuild.GuildId][t.Name] = t;
        });
    }
}
