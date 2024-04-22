using System;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.helpers;
using Adribot.src.services.providers;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace Adribot.src.services;

public sealed class RemoteAccessService(DiscordClientProvider clientProvider)
{
    private ulong? _guildId;
    private bool _isAttached;

    public async Task<(bool, string?)> ExecAsync(ActionType action, ulong? guildId, ulong? channelId, string? message)
    {
        if (action != ActionType.Connect && !_isAttached)
            return (false, "Not connected to a guild yet, use `Connect` first.");

        switch (action)
        {
            case ActionType.Connect:
                if (guildId is null)
                    return (false, "Can't connect to a guild without an id.");

                if (!clientProvider.Client.Guilds.ContainsKey((ulong)guildId))
                    return (false, "I am not in this guild!");
                
                var guildReplaced = _isAttached;
                _isAttached = true;
                _guildId = guildId;
                
                return (true, $"{(guildReplaced ? "Replacing last guild...\n" : "")}Connected to guild `{guildId}`!");
            case ActionType.Channels:
                DiscordGuild guild = await clientProvider.Client.GetGuildAsync((ulong)guildId);
                Console.WriteLine(CLIDiscordBuilder.DiscordChannels((ulong)guildId, guild.Channels.Values));

                return (true, null);
            case ActionType.Disconnect:
                clientProvider.Client.MessageCreated -= MessageCreated;
                _isAttached = false;

                return (true, $"Disconnected from guild `{_guildId}`!");
            case ActionType.Message:
                DiscordGuild iGuild = await clientProvider.Client.GetGuildAsync((ulong)guildId);
                DiscordChannel? channel = null;
                
                if (!iGuild.Channels.TryGetValue((ulong)channelId, out channel))
                    return (false, $"Can't find channel `{channelId}` in this guild.");
                
                if (string.IsNullOrEmpty(message))
                    return (false, $"Please provide a valid message string.");

                try
                {
                    await channel.SendMessageAsync(message);

                    return (true, null);
                }
                catch
                {
                    return (false, $"Couldn't send a message to channel `{channelId}`!");
                }
                
            default:
                return (false, $"Action {action} not implemented.");
        }
    }

    private Task MessageCreated(DiscordClient sender, MessageCreateEventArgs args)
    {
        if (args.Guild.Id == _guildId)
            Console.WriteLine(CLIDiscordBuilder.DiscordMessage(args.Channel.Name, args.Author.GlobalName, args.Message.Content));
        
        return Task.CompletedTask;
    }
}