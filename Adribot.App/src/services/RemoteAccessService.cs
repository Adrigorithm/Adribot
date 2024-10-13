using System;
using System.Linq;
using System.Threading.Tasks;
using Adribot.constants.enums;
using Adribot.helpers;
using Adribot.services.providers;
using Discord;
using Discord.WebSocket;

namespace Adribot.services;

public sealed class RemoteAccessService(DiscordClientProvider clientProvider)
{
    private ulong? _guildId;
    private bool _isAttached;

    public async Task<(bool, string?)> ExecAsync(RemoteAccessActionType action, ulong? guildId, ulong? channelId, string? message)
    {
        if (action != RemoteAccessActionType.Connect && !_isAttached)
            return (false, "Not connected to a guild yet, use **Connect** first.");

        switch (action)
        {
            case RemoteAccessActionType.Connect:
                if (guildId is null)
                    return (false, "Can't connect to a guild without an id.");

                if (_isAttached)
                    return (false, $"Already connected to guild **{_guildId}**, please disconnect first!");

                if (!clientProvider.Client.Guilds.Any(g => g.Id == (ulong)guildId))
                    return (false, "I am not in this guild!");

                _isAttached = true;
                _guildId = guildId;
                clientProvider.Client.MessageReceived += MessageReceived;

                return (true, $"Connected to guild **{guildId}**!");
            case RemoteAccessActionType.Channels:
                SocketGuild guild = clientProvider.Client.GetGuild((ulong)_guildId);
                Console.WriteLine(CLIDiscordBuilder.DiscordChannels((ulong)_guildId, guild.Channels));

                return (true, null);
            case RemoteAccessActionType.Members:
                SocketGuild guild0 = clientProvider.Client.GetGuild((ulong)_guildId);
                Console.WriteLine(CLIDiscordBuilder.DiscordMembers((ulong)_guildId, guild0.Users));

                return (true, null);
            case RemoteAccessActionType.Disconnect:
                clientProvider.Client.MessageReceived -= MessageReceived;
                _isAttached = false;

                return (true, $"Disconnected from guild **{_guildId}**!");
            case RemoteAccessActionType.Message:
                SocketGuild guild1 = clientProvider.Client.GetGuild((ulong)_guildId);
                SocketGuildChannel? channel = guild1.Channels.FirstOrDefault(c => c.Id == (ulong)channelId);

                if (channel is null)
                    return (false, $"Can't find channel **{channelId}** in this guild.");

                if (string.IsNullOrEmpty(message))
                    return (false, $"Please provide a valid message string.");

                try
                {
                    await ((ITextChannel)channel).SendMessageAsync(message);

                    return (true, null);
                }
                catch
                {
                    return (false, $"Couldn't send a message to channel **{channelId}**!");
                }
            default:
                return (false, $"Action **{action}** not implemented.");
        }
    }

    private Task MessageReceived(SocketMessage message)
    {
        if (message.Channel is ITextChannel channel && channel.Guild.Id == _guildId)
            Console.WriteLine(CLIDiscordBuilder.DiscordMessage(channel.Name, message.Author.GlobalName, message.Content));

        return Task.CompletedTask;
    }
}
