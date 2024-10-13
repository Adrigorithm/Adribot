using System.Linq;
using System.Threading.Tasks;
using Adribot.Entities.Discord;
using Discord;
using Discord.WebSocket;

namespace Adribot.Services;

public sealed partial class InfractionService : BaseTimerService
{
    private async Task UnbanUserAsync(Infraction infraction)
    {
        SocketGuild guild = Client.Guilds.First(g => g.Id == infraction.DMember.DGuild.GuildId);
        SocketGuildUser user = guild.GetUser(infraction.DMember.MemberId);

        await guild.RemoveBanAsync(user);
        await user.SendMessageAsync($"You have been unbanned from {guild.Name}!\nDo not let it happen again.");
    }
}
