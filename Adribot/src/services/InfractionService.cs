using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.data;
using Adribot.entities.discord;
using Adribot.extensions;
using DSharpPlus;
using DSharpPlus.Entities;

namespace Adribot.services;

public sealed class InfractionService : BaseTimerService
{
    private List<Infraction> _infractions = new();

    public InfractionService(DiscordClient client, int timerInterval = 10) : base(client, timerInterval) =>
        Client.UserUpdated += ClientUserupdatedAsync;

    private async Task ClientUserupdatedAsync(DiscordClient sender, DSharpPlus.EventArgs.UserUpdateEventArgs args) =>
        await CheckHoistAsync(args.UserAfter);

    private async Task CheckHoistAsync(DiscordUser user)
    {
        var member = user as DiscordMember;
        if (member is not null && !member.IsBot && !member.Permissions.HasPermission(Permissions.Administrator) && member.DisplayName[0] < 48)
        {
            if (!_infractions.Any(i => i.DMember.DMemberId == member.Id && i.Type == constants.enums.InfractionType.HOIST && !i.IsExpired))
            {
                using (var database = new DataManager(Client))
                {
                    DateTimeOffset currentUtcDate = DateTimeOffset.UtcNow;

                    await database.AddInstanceAsync(new Infraction
                    {
                        Date = currentUtcDate,
                        DMember = member.ToDMember(),
                        EndDate = currentUtcDate.AddHours(24),
                        IsExpired = false,
                        Type = constants.enums.InfractionType.HOIST,
                        Reason = "Hoisting is poop"
                    });
                }
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
        using (var database = new DataManager(Client))
        {
            if (_infractions.Count == 0)
                _infractions = database.GetInfractionsToOldNotExpired();

            Infraction? infraction = _infractions.FirstOrDefault(i => i.EndDate.CompareTo(DateTimeOffset.UtcNow) <= 0);
            
            if (infraction is not null)
            {   
                switch (infraction.Type)
                {
                    case constants.enums.InfractionType.HOIST:
                        await (await (await Client.GetGuildAsync(infraction.DMember.DGuild.DGuildId)).GetMemberAsync(infraction.DMember.DMemberId)).ModifyAsync(m => m.Nickname = "");
                        break;
                    case constants.enums.InfractionType.BAN:
                        DiscordGuild guild = await Client.GetGuildAsync(infraction.DMember.DGuild.DGuildId);
                        await guild.UnbanMemberAsync(infraction.DMember.DMemberId, infraction.Reason);
                        await ((DiscordMember) await Client.GetUserAsync(infraction.DMember.DMemberId)).SendMessageAsync($"You have been unbanned from {guild.Name}!\nDo not let it happen again.");
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
        bool isAdded = false;
        for (int i = 0; i < _infractions.Count; i++)
        {
            if (_infractions[i].EndDate.CompareTo(infraction.EndDate) > 0)
            {
                _infractions.Insert(i, infraction);
                isAdded = true;
            }
        }

        if (!isAdded)
            _infractions.Add(infraction);

        using var database = new DataManager(Client);
        await database.AddInstanceAsync(infraction);
    }
}
