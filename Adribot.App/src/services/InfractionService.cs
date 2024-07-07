using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.data.repositories;
using Adribot.src.entities.discord;
using Adribot.src.services.providers;
using Discord;
using Discord.WebSocket;

namespace Adribot.src.services;

public sealed class InfractionService : BaseTimerService
{
    private readonly InfractionRepository _infractionRepository;
    private readonly List<Infraction> _infractions = [];

    public InfractionService(InfractionRepository infractionRepository, DiscordClientProvider clientProvider, SecretsProvider secretsProvider, int timerInterval = 10) : base(clientProvider, secretsProvider, timerInterval)
    {
        // TODO: Figure out how to add events later
        Client.UserUpdated += ClientUserupdatedAsync;

        _infractionRepository = infractionRepository;
        _infractions = _infractionRepository.GetInfractionsToOldNotExpired().ToList();
    }

    private async Task ClientUserupdatedAsync(SocketUser user1, SocketUser user2)
    {
        if (user2 is SocketGuildUser user)
            await CheckHoistAsync(user);
    }

    private async Task CheckHoistAsync(SocketGuildUser user)
    {
        if (!user.GuildPermissions.Administrator && user.DisplayName[0] < 48)
        {
            if (!_infractions.Any(i => i.DMember.MemberId == user.Id && i.Type == InfractionType.Hoist && !i.IsExpired))
            {
                Infraction infraction = _infractionRepository.AddInfraction(user.Guild.Id, user.Id, DateTimeOffset.UtcNow.AddHours(24), InfractionType.Hoist, "Hoisting is poop");
                AddInfraction(infraction);
            }

            if (user.DisplayName != "💩")
            {
                await user.ModifyAsync(m => m.Nickname = "💩");
                await user.SendMessageAsync("You were trying to hoist, stop trying to hoist.\n" +
                    "I have therefore changed your name to 💩 for 24 hours.");
            }
        }
    }

    public override async Task Work()
    {
        if (_infractions.Count > 0)
        {
            Infraction? infraction = _infractions.FirstOrDefault(i => i.EndDate.CompareTo(DateTimeOffset.UtcNow) <= 0);

            if (infraction is not null)
            {
                switch (infraction.Type)
                {
                    case InfractionType.Hoist:
                        await Client.Guilds.First(g => g.Id == infraction.DMember.DGuild.GuildId).Users.First(u => u.Id == infraction.DMember.MemberId).ModifyAsync(m => m.Nickname = "");
                        break;
                    case InfractionType.Ban:
                        SocketGuild guild = Client.Guilds.First(g => g.Id == infraction.DMember.DGuild.GuildId);
                        SocketGuildUser user = guild.GetUser(infraction.DMember.MemberId);

                        await guild.RemoveBanAsync(user);
                        await user.SendMessageAsync($"You have been unbanned from {guild.Name}!\nDo not let it happen again.");
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
