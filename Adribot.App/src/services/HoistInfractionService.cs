using System;
using System.Linq;
using System.Threading.Tasks;
using Adribot.constants.enums;
using Adribot.entities.discord;
using Discord;
using Discord.WebSocket;

namespace Adribot.services;

public sealed partial class InfractionService : BaseTimerService
{
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

    private void RemovePoopNickname(Infraction infraction) =>
        Client.Guilds.First(g => g.Id == infraction.DMember.DGuild.GuildId).Users.First(u => u.Id == infraction.DMember.MemberId).ModifyAsync(m => m.Nickname = "");
}
