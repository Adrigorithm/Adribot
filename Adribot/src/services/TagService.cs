using System;
using System.Collections.Generic;
using System.Linq;
using Adribot.src.data.repositories;
using Adribot.src.entities.utilities;
using Adribot.src.services.providers;
using DSharpPlus;

namespace Adribot.src.services;

public sealed class TagService
{
    private readonly TagRepository _tagRepository;
    private readonly DiscordClient _client;

    private Dictionary<ulong, Dictionary<string, Tag>> _tags { get; } = [];

    public TagService(TagRepository tagRepository, DiscordClientProvider discordClientProvider)
    {
        _client = discordClientProvider.Client;
        _tagRepository = tagRepository;

        _tagRepository.GetAllTags().ToList().ForEach(t =>
        {
            if (!_tags.ContainsKey(t.DMember.DGuild.GuildId))
                _tags[t.DMember.DGuild.GuildId] = [];

            _tags[t.DMember.DGuild.GuildId][t.Name] = t;
        });

    }

    public void SetTag(ulong guildId, ulong memberId, Tag tag)
    {
        if (_tags[guildId].ContainsKey(tag.Name))
        {
            _tags[guildId][tag.Name].Overwrite(tag);
            _tagRepository.UpdateTag(_tags[guildId][tag.Name]);
        }
        else
        {
            _tagRepository.AddTag(guildId, memberId, tag);
            _tags[guildId][tag.Name] = tag;
        }
    }

    public IEnumerable<Tag> GetAllTags(ulong guildId) =>
        _tags[guildId].Values;


    public Tag? TryGetTag(string tagName, ulong guildId) =>
        _tags[guildId].GetValueOrDefault(tagName);

    public bool TryRemoveTag(string tagname, ulong guildId)
    {
        Tag? tag = TryGetTag(tagname, guildId);
        if (tag is not null)
        {
            _tagRepository.Remove(tag);
            _tags[guildId].Remove(tagname);

            return true;
        }
        return false;
    }

    public (Tag?, string?) CreateTempTag(ulong guildId, ulong memberId, string tagName, string tagContent, DateTimeOffset createdAt, bool allowOverride) => string.IsNullOrWhiteSpace(tagContent) || string.IsNullOrWhiteSpace(tagName)
            ? (null, $"The {nameof(tagName)} and {nameof(tagContent)} cannot be empty.")
            : !_tags[guildId].ContainsKey(tagName) || (allowOverride && _tags[guildId][tagName].DMember.MemberId == memberId) ? (new Tag
            {
                Content = tagContent,
                Date = createdAt,
                Name = tagName
            }, null) : (null, "Tag name already taken");

}
