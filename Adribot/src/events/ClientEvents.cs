using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Exceptions;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

class ClientEvents
{
    private Dictionary<ulong, DateTime> _poopers = new();
    private DiscordClient _client;

    public bool UseGuildMemberUpdated;
    public bool UseMessageCreated;
    public bool UseUserUpdated;

    // Slashies
    public bool UseSlashCommandErrored;

    public ClientEvents(DiscordClient client)
    {
        _client = client;
    }

    public void Attach()
    {
        var slashies = _client.GetExtension<SlashCommandsExtension>();

        if (UseMessageCreated)
            _client.MessageCreated += MessageCreatedAsync;
        if (UseGuildMemberUpdated)
            _client.GuildMemberUpdated += GuildMemberUpdatedAsync;
        if (UseUserUpdated)
            _client.UserUpdated += UserUpdatedAsync;
        if (UseSlashCommandErrored)
            slashies.SlashCommandErrored += SlashCommandErroredAsync;
    }

    private Task SlashCommandErroredAsync(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
    {
        Console.WriteLine($"{e.Context.CommandName}\n{e.Exception.Message}");

        return Task.CompletedTask;
    }

    private async Task MessageCreatedAsync(DiscordClient client, MessageCreateEventArgs args)
    {
        if (args.Guild != null && !args.Author.IsBot && args.MentionedUsers.Count > 0 && !((DiscordMember)args.Author).Permissions.HasPermission(Permissions.Administrator))
        {
            foreach (var user in args.MentionedUsers)
            {
                if (((DiscordMember)user).Permissions.HasPermission(Permissions.Administrator) || user.Id == 135081249017430016)
                    try
                    {
                        await args.Message.CreateReactionAsync(DiscordEmoji.FromName(client, ":killercat:"));
                    }
                    catch (NotFoundException)
                    {
                        await args.Message.CreateReactionAsync(DiscordEmoji.FromName(client, ":knife:"));
                    }
            }
        }
    }

    private async Task UserUpdatedAsync(DiscordClient sender, UserUpdateEventArgs args) => await HoistCheckAsync((DiscordMember)args.UserAfter);

    private async Task GuildMemberUpdatedAsync(DiscordClient client, GuildMemberUpdateEventArgs args) => await HoistCheckAsync(args.MemberAfter);

    private async Task HoistCheckAsync(DiscordMember memberAfter)
    {
        if (!String.IsNullOrWhiteSpace(memberAfter.DisplayName) && (byte)memberAfter.DisplayName[0] < 48)
        {
            await memberAfter.ModifyAsync(m => m.Nickname = "💩");
            _poopers[memberAfter.Id] = DateTime.UtcNow;
        }
        else if (_poopers.ContainsKey(memberAfter.Id) && _poopers[memberAfter.Id].AddDays(1).CompareTo(DateTime.UtcNow) > 0)
        {
            if (memberAfter.DisplayName != "💩" )
                await memberAfter.ModifyAsync(m => m.Nickname = "💩");
        }
        else
        {
            _poopers.Remove(memberAfter.Id);
            await Task.CompletedTask;
        }
    }
}