using System.Threading.Tasks;
using Adribot.src.services.providers;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Adribot.src.services;

public sealed class RemoteAccessService(DiscordClientProvider clientProvider, SecretsProvider secretsProvider)
{
    private ulong? _guildId;
    private ulong? _channelId;
    private bool _isAttached;

    public void Attach(ulong guildId, ulong? channelId = null)
    {
        (_guildId, _channelId) = (guildId, channelId);
        clientProvider.Client.MessageCreated += MessageCreatedAsync;
        
        _isAttached = true;
    }

    public bool TryReattach()
    {
        if (_isAttached || _guildId is null)
            return false;
        
        clientProvider.Client.MessageCreated += MessageCreatedAsync;
        return true;
    }

    private async Task MessageCreatedAsync(DiscordClient sender, MessageCreateEventArgs args)
    {
        
    }

    public void Detach()
    {
        clientProvider.Client.MessageCreated -= MessageCreatedAsync;

        _isAttached = false;
    }
}