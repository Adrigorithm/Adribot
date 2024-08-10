using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adribot.src.constants.enums;
using Adribot.src.data.repositories;
using Adribot.src.entities.discord;
using Adribot.src.services.providers;

namespace Adribot.src.services;

public sealed partial class InfractionService : BaseTimerService
{
    private readonly InfractionRepository _infractionRepository;
    private readonly IEnumerable<Infraction> _infractions = [];

    public InfractionService(InfractionRepository infractionRepository, DiscordClientProvider clientProvider, SecretsProvider secretsProvider, int timerInterval = 10) : base(clientProvider, secretsProvider, timerInterval)
    {
        Client.UserUpdated += ClientUserupdatedAsync;

        _infractionRepository = infractionRepository;
        _infractions = _infractionRepository.GetInfractionsToOldNotExpired();
    }

    public override async Task Work()
    {
        if (_infractions.Count() > 0)
        {
            Infraction? infraction = _infractions.FirstOrDefault(i => i.EndDate.CompareTo(DateTimeOffset.UtcNow) <= 0);

            if (infraction is not null)
            {
                switch (infraction.Type)
                {
                    case InfractionType.Hoist:
                        RemovePoopNickname(infraction);
                        break;
                    case InfractionType.Ban:
                        await UnbanUserAsync(infraction);
                        break;
                    default:
                        break;
                }

                _infractions.ToList().Remove(infraction);
                _infractionRepository.SetExpiredStatus(infraction, true);
            }
        }
    }

    public void AddInfraction(ulong guildId, ulong memberId, DateTimeOffset endDate, InfractionType type, string reason, bool isExpired = false)
    {
        Infraction infraction = _infractionRepository.AddInfraction(guildId, memberId, endDate, type, reason, isExpired);

        AddInfraction(infraction);
    }

    private void AddInfraction(Infraction infraction)
    {
        var isAdded = false;
        for (var i = 0; i < _infractions.Count(); i++)
        {
            if (_infractions.ElementAt(i).EndDate.CompareTo(infraction.EndDate) > 0)
            {
                _infractions.ToList().Insert(i, infraction);
                isAdded = true;
            }
        }

        if (!isAdded)
            _infractions.Append(infraction);
    }
}
