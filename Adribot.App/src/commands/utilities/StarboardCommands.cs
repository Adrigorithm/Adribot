using System.Threading.Tasks;
using Adribot.Services;
using Discord;
using Discord.Interactions;

namespace Adribot.Commands.Utilities;

public class StarboardCommands(StarboardService starboardService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("starboard", "Configure the starboard service")]
    [RequireUserPermission(GuildPermission.Administrator)]
    [RequireContext(ContextType.Guild)]
    public async Task ConfigureAsync([Summary("channel", "defaults to current channel")] ITextChannel? channel = null, [Summary("emojis", "list of emojis to track, separated by spaces")] string? emojis = null, [Summary("emotes", "list of emotes to track, separated by spaces")] string? emotes = null, [Summary("threshold", "Amount of staremoji to trigger the service"), MinValue(1), MaxValue(int.MaxValue)] int amount = 3)
    {
        starboardService.Configure(Context.Guild.Id, channel?.Id ?? Context.Channel.Id, emoji, amount);

        await RespondAsync($"Starred messages will now appear in <#{channel?.Id ?? Context.Channel.Id}>");
    }
}
