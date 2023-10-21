using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.data;
using Adribot.src.entities.utilities;
using DSharpPlus;

namespace Adribot.src.services
{
    public sealed class TagService
    {
        private readonly DiscordClient _client;

        /// <summary>
        /// TagId on the individual tags will be 0 when they are added later to minimise database interactions.
        /// Do NOT depend on the ids.
        /// </summary>
        public List<Tag> Tags { get; }

        public TagService(DiscordClient client)
        {
            _client = client;

            using var database = new DataManager();
            Tags = database.GetAllInstances<Tag>().ToList();
        }

        public async Task<bool> TrySetTagAsync(Tag tag, bool shouldOverwrite = false)
        {
            Tag? oldTag = null;
            int oldTagIndex = -1;

            for (int i = 0; i < Tags.Count; i++)
            {
                if (Tags[i].Name == tag.Name &&
                    Tags[i].DGuildId == tag.DGuildId)
                {
                    oldTag = Tags[i];
                    oldTagIndex = i;
                }
            }

            if (oldTag is null || shouldOverwrite)
            {
                using var database = new DataManager();

                if (oldTag is null)
                {
                    await database.AddInstanceAsync(tag);
                    Tags.Add(tag);
                }
                else
                {
                    tag.TagId = oldTag.TagId;
                    Tags[oldTagIndex] = tag;
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

                using var database = new DataManager();
                database.RemoveInstance(tag);

                return true;
            }
            return false;
        }
    }
}
