using Adribot.data;
using Adribot.entities.utilities;
using DSharpPlus;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adribot.src.services
{
    public sealed class TagService
    {
        private readonly DiscordClient _client;

        public List<Tag> Tags { get; }

        public TagService(DiscordClient client)
        {
            _client = client;

            using var database = new DataManager(_client);
            Tags = database.GetAllInstances<Tag>().ToList();
        }

        public async Task<bool> TrySetTagAsync(Tag tag, bool shouldOverwrite = false)
        {
            Tag? oldTag = null;
            int oldTagId = -1;

            for (int i = 0; i < Tags.Count; i++)
            {
                if (Tags[i].Name == tag.Name &&
                    Tags[i].DGuildId == tag.DGuildId)
                {
                    oldTag = Tags[i];
                    oldTagId = i;
                }
            }

            if (oldTag is null || shouldOverwrite)
            {
                using var database = new DataManager(_client);

                if (oldTag is null)
                {
                    Tags.Add(tag);
                    await database.AddInstanceAsync(tag);
                }
                else
                {
                    tag.TagId = oldTag.TagId;
                    Tags[oldTagId] = tag;
                    database.UpdateInstance(tag);
                }
                
                return true;
            }
            return false;
        }

        public IEnumerable<Tag> GetAllTags(ulong guildId) =>
            Tags.Where(t => t.DGuildId == guildId);
            

        public Tag? TryGetTag(string tagName, ulong guildId) =>
            Tags.FirstOrDefault(t => t.Name == tagName && t.DGuildId == guildId);

        public bool TryRemoveTag(string tagname, ulong guildId)
        {
            Tag? tag = TryGetTag(tagname, guildId);
            if (tag is not null)
            {
                Tags.Remove(tag);

                using (var database = new DataManager(_client))
                    database.RemoveInstance(tag);

                return true;
            }
            return false;
        }
    }
}
