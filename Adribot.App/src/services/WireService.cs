using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Adribot.Constants.Enums;
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
    private readonly IEnumerable<WireConfig> _wireConfigs = wireRepository.GetAllWireConfigs();

    public async Task<(bool, string)> TryCreateWireConfigAsync(ulong guildId, string name, Emote emote)
    {
        SocketGuild? guild = _client.GetGuild(guildId);

        if (guild is null)
            return (false, $"I am not in Guild with id {guildId}!");

        var isDuplicateName = _wireConfigs.Any(w => w.EmoteName == name);
        
        if (isDuplicateName)
            return (false, $"A wire config with name {name} already exists!");
        
        DGuild? dGuild = guildRepository.GetGuild(guildId);
        var emoteData = await httpClientFactory.CreateClient().GetByteArrayAsync(emote.Url);
        
        var wireConfig = new WireConfig
        {
            DGuild = dGuild,
            DGuildId = dGuild.DGuildId,
            EmoteData = emoteData,
            EmoteName = name
        };
        
        wireRepository.AddWireConfig(wireConfig);
        
        _ = _wireConfigs.Append(wireConfig);
        
        return (true, null);
    }

    public async Task<(bool, string)> CreateWireAsync(string name, bool shouldReplace)
    {
        WireConfig? wireConfig = _wireConfigs.FirstOrDefault(w => w.EmoteName == name);
        
        if (wireConfig is null)
            return (false, $"Couldn't find a wire config for name {name}!");

        SocketGuild? wireGuild = _client.GetGuild(wireConfig.DGuild.GuildId);
        
        if (wireGuild is null)
            return (false, $"I am no longer in Guild with id {wireConfig.DGuildId}!");

        if (shouldReplace)
        {
            GuildEmote? emote = wireGuild.Emotes.FirstOrDefault(e => e.Name == name);
            
            if (emote is not null)
                await wireGuild.DeleteEmoteAsync(emote);
        }
            
        
        await wireGuild.CreateEmoteAsync(name, new Image(new MemoryStream(wireConfig.EmoteData)));
        
        return (true, null);
    }
}
