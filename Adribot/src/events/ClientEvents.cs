using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Exceptions;

class ClientEvents
{
    private NoHoistService _noHoistServer;
    private DiscordClient _client;

    public bool UseGuildMemberUpdated;
    public bool UseMessageCreated;

    public ClientEvents(DiscordClient client)
    {
        _client = client;
        _noHoistServer = new();
    }

    public void Attach()
    {
        if (UseMessageCreated)
            _client.MessageCreated += MessageCreatedAsync;
        if (UseGuildMemberUpdated)
            _client.GuildMemberUpdated += GuildMemberUpdatedAsync;
    }

    private async Task MessageCreatedAsync(DiscordClient client, MessageCreateEventArgs args)
    {
        if (!args.Author.IsBot)
        {
            foreach (var user in args.MentionedUsers)
            {
                if (user.Id == 135081249017430016 || ((DiscordMember)user).Permissions.HasPermission(Permissions.Administrator))
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

    private Task GuildMemberUpdatedAsync(DiscordClient client, GuildMemberUpdateEventArgs args)
    {
        _noHoistServer.HoistCheck(args.MemberAfter);
        return Task.CompletedTask;
    }
}