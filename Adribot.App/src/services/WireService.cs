using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Adribot.Data.Repositories;
using Adribot.Entities.Discord;
using Adribot.Entities.fun;
using Adribot.Services.Providers;
using Discord;
using Discord.WebSocket;

namespace Adribot.Services;

public sealed class WireService(DiscordClientProvider provider, IHttpClientFactory httpClientFactory, DGuildRepository guildRepository, WireRepository wireRepository)
{
    private readonly DiscordSocketClient _client = provider.Client;
    private readonly List<WireConfig> _wireConfigs = wireRepository.GetAllWireConfigs();

    public async Task<(bool, string)> TryCreateWireConfigAsync(ulong guildId, ulong userId, string name, Emote emote)
    {
        SocketGuild? guild = _client.GetGuild(guildId);

        if (guild is null)
            return (false, $"I am not in Guild with id `{guildId}`!");

        if (!HasManageEmojiAndStickersPermission(guild, userId))
            return (false, $"User does not exist in guild `{guildId}` or has insufficient permissions!");

        var isDuplicateName = _wireConfigs.Any(w => w.EmoteName == name);

        if (isDuplicateName)
            return (false, $"A wire config with name `{name}` already exists!");

        DGuild? dGuild = guildRepository.GetGuild(guildId);
        var emoteData = await httpClientFactory.CreateClient().GetByteArrayAsync(emote.Url);

        var wireConfig = new WireConfig
        {
            DGuildId = dGuild.DGuildId, EmoteData = emoteData, EmoteName = name
        };

        wireRepository.AddWireConfig(wireConfig);
        _wireConfigs.Add(wireConfig);

        return (true, null);
    }

    public async Task<(bool, string)> CreateWireAsync(ulong userId, string name, bool shouldReplace)
    {
        WireConfig? wireConfig = _wireConfigs.FirstOrDefault(w => w.EmoteName == name);

        if (wireConfig is null)
            return (false, $"Couldn't find a wire config for name `{name}`!");

        SocketGuild? wireGuild = _client.GetGuild(wireConfig.DGuild.GuildId);

        if (wireGuild is null)
            return (false, $"I am no longer in Guild with id `{wireConfig.DGuildId}`!");

        if (!HasManageEmojiAndStickersPermission(wireGuild, userId))
            return (false, $"User does not exist in guild `{wireGuild.Id}` or has insufficient permissions!");

        if (shouldReplace)
        {
            GuildEmote? emote = wireGuild.Emotes.FirstOrDefault(e => e.Name == name);

            if (emote is not null)
                await wireGuild.DeleteEmoteAsync(emote);
        }

        await wireGuild.CreateEmoteAsync(name, new Image(new MemoryStream(wireConfig.EmoteData)));

        return (true, null);
    }

    private bool HasManageEmojiAndStickersPermission(SocketGuild guild, ulong userId)
    {
        SocketGuildUser? user = guild.Users.FirstOrDefault(x => x.Id == userId);

        return user is not null && user.GuildPermissions.ManageEmojisAndStickers;
    }
}
