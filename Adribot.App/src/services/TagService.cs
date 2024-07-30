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

    private Dictionary<ulong, Dictionary<string, Tag>> _tags { get; } = [];

    public TagService(TagRepository tagRepository)
    {
        _tagRepository = tagRepository;

        _tagRepository.GetAllTags().ToList().ForEach(t =>
        {
            if (!_tags.ContainsKey(t.DMember.DGuild.GuildId))
                _tags[t.DMember.DGuild.GuildId] = [];

            _tags[t.DMember.DGuild.GuildId][t.Name] = t;
        });

    }

    // TODO: Please optimise this
    public void SetTag(ulong guildId, ulong memberId, Tag tag)
    {
        if (_tags.TryGetValue(guildId, out Dictionary<string, Tag>? tags))
        {
            if (tags.TryGetValue(tag.Name, out Tag? cachedTag))
            {
                cachedTag.Overwrite(tag);
                _tagRepository.UpdateTag(cachedTag);
                _tags[guildId][cachedTag.Name] = cachedTag;

                return;
            }

            Tag addedTag = _tagRepository.AddTag(guildId, memberId, tag);
            _tags[guildId][addedTag.Name] = addedTag;

            return;
        }

        Tag newTag = _tagRepository.AddTag(guildId, memberId, tag);

        _tags[guildId] = [];
        _tags[guildId][newTag.Name] = newTag;
    }

    public ImmutableArray<Tag> GetAllTags(ulong guildId) =>
        _tags.ContainsKey(guildId)
            ? _tags[guildId].Values.ToImmutableArray()
            : [];

    public Tag? TryGetTag(string tagName, ulong guildId) =>
        _tags.ContainsKey(guildId)
            ? _tags[guildId].GetValueOrDefault(tagName)
            : null;

    public bool TryRemoveTag(string tagname, ulong guildId)
    {
        if (string.IsNullOrWhiteSpace(tagname))
            return false;

        Tag? tag = TryGetTag(tagname, guildId);

        if (tag is not null)
        {
            _tagRepository.Remove(tag);
            _tags[guildId].Remove(tagname);

            return true;
        }

        return false;
    }

    public (Tag?, string?) CreateTempTag(ulong guildId, ulong memberId, string tagName, string tagContent, DateTimeOffset createdAt, bool allowOverride)
    {
        return FakeExtensions.AreAllNullOrWhiteSpace(tagName, tagContent)
            ? (null, $"The {nameof(tagName)} and {nameof(tagContent)} cannot be empty.")
            : !_tags.ContainsKey(guildId) || !_tags[guildId].TryGetValue(tagName, out Tag tag) || (allowOverride && tag.DMember.MemberId == memberId)
            ? (new Tag
            {
                Content = tagContent,
                Date = createdAt,
                Name = tagName
            }, null)
            : (null, "Tag name already taken!");
    }
}
