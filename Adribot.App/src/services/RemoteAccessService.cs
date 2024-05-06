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
            return (false, "Not connected to a guild yet, use **Connect** first.");

        switch (action)
        {
            case ActionType.Connect:
                if (guildId is null)
                    return (false, "Can't connect to a guild without an id.");

                if (_isAttached)
                    return (false, $"Already connected to guild **{_guildId}**, please disconnect first!");

                if (!clientProvider.Client.Guilds.ContainsKey((ulong)guildId))
                    return (false, "I am not in this guild!");

                _isAttached = true;
                _guildId = guildId;
                clientProvider.Client.MessageCreated += MessageCreated;

                return (true, $"Connected to guild **{guildId}**!");
            case ActionType.Channels:
                DiscordGuild guild = await clientProvider.Client.GetGuildAsync((ulong)_guildId);
                Console.WriteLine(CLIDiscordBuilder.DiscordChannels((ulong)_guildId, guild.Channels.Values));

                return (true, null);
            case ActionType.Members:
                DiscordGuild guild0 = await clientProvider.Client.GetGuildAsync((ulong)_guildId);
                Console.WriteLine(CLIDiscordBuilder.DiscordMembers((ulong)_guildId, guild0.Members.Values));

                return (true, null);
            case ActionType.Disconnect:
                clientProvider.Client.MessageCreated -= MessageCreated;
                _isAttached = false;

                return (true, $"Disconnected from guild **{_guildId}**!");
            case ActionType.Message:
                DiscordGuild guild1 = await clientProvider.Client.GetGuildAsync((ulong)_guildId);
                DiscordChannel? channel = null;

                if (!guild1.Channels.TryGetValue((ulong)channelId, out channel))
                    return (false, $"Can't find channel **{channelId}** in this guild.");

                if (string.IsNullOrEmpty(message))
                    return (false, $"Please provide a valid message string.");

                try
                {
                    await channel.SendMessageAsync(message);

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

    private Task MessageCreated(DiscordClient sender, MessageCreateEventArgs args)
    {
        if (args.Guild.Id == _guildId)
            Console.WriteLine(CLIDiscordBuilder.DiscordMessage(args.Channel.Name, args.Author.GlobalName, args.Message.Content));

        return Task.CompletedTask;
    }
}
