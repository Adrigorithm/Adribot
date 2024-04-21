using System;
using System.Threading.Tasks;
using Adribot.src.helpers;
using Adribot.src.services.providers;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Adribot.src.services;

public sealed class RemoteAccessService(DiscordClientProvider clientProvider)
{
    private ulong? _guildId;
    private ulong? _channelId;
    private bool _isAttached;

    public void Attach(ulong guildId, ulong? channelId = null)
    {
        (_guildId, _channelId) = (guildId, channelId);
        clientProvider.Client.MessageCreated += MessageCreated;
        
        _isAttached = true;
    }

    public bool TryReattach()
    {
        if (_isAttached || _guildId is null)
            return false;
        
        clientProvider.Client.MessageCreated += MessageCreated;
        return true;
    }

    private Task MessageCreated(DiscordClient sender, MessageCreateEventArgs args)
    {
        if (args.Guild.Id == _guildId && (_channelId is null || args.Channel.Id == _channelId))
            Console.WriteLine(CLIDiscordMessageBuilder.Build(args.Channel.Name, args.Author.GlobalName, args.Message.Content));
        
        return Task.CompletedTask;
    }

    public void Detach()
    {
        clientProvider.Client.MessageCreated -= MessageCreated;

        _isAttached = false;
    }
}