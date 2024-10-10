using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Adribot.src.data.repositories;
using Adribot.src.entities.utilities;
using Adribot.src.helpers;

namespace Adribot.src.services;

public sealed class TagService
{
    private readonly TagRepository _tagRepository;

    private Dictionary<ulong, Dictionary<string, Tag>> Tags { get; } = [];

    public TagService(TagRepository tagRepository)
    {
        _tagRepository = tagRepository;

        _tagRepository.GetAllTags().ToList().ForEach(t =>
        {
            if (!Tags.ContainsKey(t.DMember.DGuild.GuildId))
                Tags[t.DMember.DGuild.GuildId] = [];

            Tags[t.DMember.DGuild.GuildId][t.Name] = t;
        });

    }

    // TODO: Please optimise this
    public void SetTag(ulong guildId, ulong memberId, Tag tag)
    {
        if (Tags.TryGetValue(guildId, out Dictionary<string, Tag>? tags))
        {
            if (tags.TryGetValue(tag.Name, out Tag? cachedTag))
            {
                cachedTag.Overwrite(tag);
                _tagRepository.UpdateTag(cachedTag);
                Tags[guildId][cachedTag.Name] = cachedTag;

                return;
            }

            Tag addedTag = _tagRepository.AddTag(guildId, memberId, tag);
            Tags[guildId][addedTag.Name] = addedTag;

            return;
        }

        Tag newTag = _tagRepository.AddTag(guildId, memberId, tag);

        Tags[guildId] = [];
        Tags[guildId][newTag.Name] = newTag;
    }

    public ImmutableArray<Tag> GetAllTags(ulong guildId) =>
        Tags.ContainsKey(guildId)
            ? Tags[guildId].Values.ToImmutableArray()
            : [];

    public Tag? TryGetTag(string tagName, ulong guildId) =>
        Tags.ContainsKey(guildId)
            ? Tags[guildId].GetValueOrDefault(tagName)
            : null;

    public bool TryRemoveTag(string tagname, ulong guildId)
    {
        if (string.IsNullOrWhiteSpace(tagname))
            return false;

        Tag? tag = TryGetTag(tagname, guildId);

        if (tag is not null)
        {
            _tagRepository.Remove(tag);
            Tags[guildId].Remove(tagname);

            return true;
        }

        return false;
    }

    public (Tag?, string?) CreateTempTag(ulong guildId, ulong memberId, string tagName, string tagContent, DateTimeOffset createdAt, bool allowOverride) =>
        FakeExtensions.AreAllNullOrWhiteSpace(tagName, tagContent)
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
