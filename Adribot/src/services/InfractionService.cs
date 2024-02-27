using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.data.repositories;
using Adribot.src.entities.discord;
using Adribot.src.services.providers;
using DSharpPlus;
using DSharpPlus.Entities;

namespace Adribot.src.services;

public sealed class InfractionService : BaseTimerService
{
    private readonly InfractionRepository _infractionRepository;
    private readonly List<Infraction> _infractions = [];

    public InfractionService(InfractionRepository infractionRepository, DiscordClientProvider clientProvider, SecretsProvider secretsProvider, int timerInterval = 10) : base(clientProvider, secretsProvider, timerInterval)
    {
        Client.UserUpdated += ClientUserupdatedAsync;

        _infractionRepository = infractionRepository;
        _infractions = _infractionRepository.GetInfractionsToOldNotExpired().ToList();
    }

    private async Task ClientUserupdatedAsync(DiscordClient sender, DSharpPlus.EventArgs.UserUpdateEventArgs args) =>
        await CheckHoistAsync(args.UserAfter);

    private async Task CheckHoistAsync(DiscordUser user)
    {
        var member = user as DiscordMember;
        if (member is not null && !member.IsBot && !member.Permissions.HasPermission(Permissions.Administrator) && member.DisplayName[0] < 48)
        {
            if (!_infractions.Any(i => i.DMember.MemberId == member.Id && i.Type == InfractionType.HOIST && !i.IsExpired))
            {
                Infraction infraction = _infractionRepository.AddInfraction(member.Guild.Id, member.Id, DateTimeOffset.UtcNow.AddHours(24), InfractionType.HOIST, "Hoisting is poop");
                AddInfraction(infraction);
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

    public override async Task Work()
    {
        if (_infractions.Count > 0)
        {
            Infraction? infraction = _infractions.FirstOrDefault(i => i.EndDate.CompareTo(DateTimeOffset.UtcNow) <= 0);

            if (infraction is not null)
            {
                switch (infraction.Type)
                {
                    case InfractionType.HOIST:
                        await (await (await Client.GetGuildAsync(infraction.DMember.DGuild.GuildId)).GetMemberAsync(infraction.DMember.MemberId)).ModifyAsync(m => m.Nickname = "");
                        break;
                    case InfractionType.BAN:
                        DiscordGuild guild = await Client.GetGuildAsync(infraction.DMember.DGuild.GuildId);
                        await guild.UnbanMemberAsync(infraction.DMember.MemberId, infraction.Reason);
                        await ((DiscordMember)await Client.GetUserAsync(infraction.DMember.MemberId)).SendMessageAsync($"You have been unbanned from {guild.Name}!\nDo not let it happen again.");
                        break;
                    default:
                        break;
                }

                _infractions.Remove(infraction);
                _infractionRepository.SetExpiredStatus(infraction, true);
            }
        }
    }

    public void AddInfraction(ulong guildId, ulong memberId, DateTimeOffset endDate, InfractionType type, string reason, bool isExpired = false)
    {
        Infraction infraction = _infractionRepository.AddInfraction(guildId, memberId, endDate, type, reason, isExpired);

        AddInfraction(infraction);
    }

    public void AddInfraction(Infraction infraction)
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
    }
}
