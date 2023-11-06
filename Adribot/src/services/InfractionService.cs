using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.data;
using Adribot.src.entities.discord;
using DSharpPlus;
using DSharpPlus.Entities;

namespace Adribot.src.services;

public sealed class InfractionService : BaseTimerService
{
    private readonly List<Infraction> _infractions = new();

    public InfractionService(DiscordClient client, int timerInterval = 10) : base(client, timerInterval)
    {
        Client.UserUpdated += ClientUserupdatedAsync;

        using var database = new DataManager();
        _infractions = database.GetInfractionsToOldNotExpired();
    }

    private async Task ClientUserupdatedAsync(DiscordClient sender, DSharpPlus.EventArgs.UserUpdateEventArgs args) =>
        await CheckHoistAsync(args.UserAfter);

    private async Task CheckHoistAsync(DiscordUser user)
    {
        var member = user as DiscordMember;
        if (member is not null && !member.IsBot && !member.Permissions.HasPermission(Permissions.Administrator) && member.DisplayName[0] < 48)
        {
            if (!_infractions.Any(i => i.DMemberId == member.Id && i.Type == InfractionType.HOIST && !i.IsExpired))
            {
                using var database = new DataManager();
                DateTimeOffset currentUtcDate = DateTimeOffset.UtcNow;

                await database.AddInstanceAsync(new Infraction
                {
                    Date = currentUtcDate,
                    DMemberId = member.Id,
                    EndDate = currentUtcDate.AddHours(24),
                    IsExpired = false,
                    Type = InfractionType.HOIST,
                    Reason = "Hoisting is poop",
                    DGuildId = member.Guild.Id
                });
            }

            if (member.DisplayName != "💩")
            {
                await member.ModifyAsync(m => m.Nickname = "💩");
                await member.SendMessageAsync("You were trying to hoist, stop trying to hoist.\n" +
                    "I have therefore changed your name to 💩 for 24 hours.");
            }
        }
    }
    public override async Task Start(int timerInterval) =>
        await base.Start(timerInterval);

    public override async Task WorkAsync()
    {
        if (_infractions.Count > 0)
        {
            using var database = new DataManager();
            Infraction? infraction = _infractions.FirstOrDefault(i => i.EndDate.CompareTo(DateTimeOffset.UtcNow) <= 0);

            if (infraction is not null)
            {
                switch (infraction.Type)
                {
                    case InfractionType.HOIST:
                        await (await (await Client.GetGuildAsync(infraction.DGuildId)).GetMemberAsync(infraction.DMemberId)).ModifyAsync(m => m.Nickname = "");
                        break;
                    case InfractionType.BAN:
                        DiscordGuild guild = await Client.GetGuildAsync(infraction.DGuildId);
                        await guild.UnbanMemberAsync(infraction.DMemberId, infraction.Reason);
                        await ((DiscordMember)await Client.GetUserAsync(infraction.DMemberId)).SendMessageAsync($"You have been unbanned from {guild.Name}!\nDo not let it happen again.");
                        break;
                    default:
                        break;
                }

                _infractions.Remove(infraction);
                infraction.IsExpired = true;
                database.UpdateInstance(infraction);
            }
        }
    }

    public async Task AddInfractionAsync(Infraction infraction)
    {
        var isAdded = false;
        for (var i = 0; i < _infractions.Count; i++)
        {
            if (_infractions[i].EndDate.CompareTo(infraction.EndDate) > 0)
            {
                _infractions.Insert(i, infraction);
                isAdded = true;
            }
        }

        if (!isAdded)
            _infractions.Add(infraction);

        using var database = new DataManager();
        await database.AddInstanceAsync(infraction);
    }
}
