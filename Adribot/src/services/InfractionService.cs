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
        await Task.Run(async () => await CheckHoistAsync(args.UserAfter));

    private async Task CheckHoistAsync(DiscordUser user)
    {
        var member = user as DiscordMember;
        if (member is not null && (!member.IsBot || !member.Permissions.HasPermission(Permissions.Administrator)) && member.DisplayName[0] < 48)
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
                await member.ModifyAsync(m => m.Nickname = "💩");
        }
    }
    public override async Task Start(int timerInterval) =>
        await base.Start(timerInterval);

    public override async Task WorkAsync()
    {
        using (var database = new DataManager(Client))
        {
            if (_infractions.Count == 0 || _infractions.All(i => i.IsExpired))
                _infractions = database.GetInfractions();

            IEnumerable<Infraction> infractions = _infractions.Where(i => i.EndDate.CompareTo(DateTimeOffset.UtcNow) <= 0);
            foreach (Infraction infraction in infractions)
            {
                switch (infraction.Type)
                {
                    case constants.enums.InfractionType.HOIST:
                        await (await (await Client.GetGuildAsync(infraction.DMember.DGuildId)).GetMemberAsync(infraction.DMember.DMemberId)).ModifyAsync(m => m.Nickname = "");
                        break;
                    case constants.enums.InfractionType.BAN:
                        await (await Client.GetGuildAsync(infraction.DMember.DGuildId)).UnbanMemberAsync(infraction.DMember.DMemberId, infraction.Reason);
                        break;
                    default:
                        break;
                }

                infraction.IsExpired = true;
                database.UpdateInstance(infraction);
            }
        }
    }

    //private async Task<List<DGuild>> InitiateInfractionsAsync() => await Task.CompletedTask;
}
