using System;
using System.Linq;
using System.Threading.Tasks;
using Adribot.Constants.Enums;
using Adribot.Entities.Discord;
using Adribot.Services.Providers;
using Discord;
using Discord.WebSocket;

namespace Adribot.Services;

public sealed class WireService(DiscordClientProvider clientProvider)
{
    private readonly DiscordSocketClient _client = clientProvider.Client;

    public void ReplaceEmote(Emote emote)
    {
        
    }

    public bool TrySetWireServer(ulong guildId)
    {
        SocketGuild? guild = _client.Guilds.FirstOrDefault(g => g.Id == guildId);
        
        if (guild is null)
            return false;
        
        
    }
}
