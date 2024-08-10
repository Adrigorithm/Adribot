using System.Threading.Tasks;
using Adribot.src.services;
using Discord;
using Discord.Interactions;

namespace Adribot.src.commands.utilities;

public class StarboardCommands(StarboardService _starboardService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("starboard", "Configure the starboard service")]
    [RequireUserPermission(GuildPermission.Administrator)]
    [RequireContext(ContextType.Guild)]
    public async Task ConfigureAsync([Summary("channel", "defaults to current channel")] ITextChannel? channel = null, [Summary("emoji", "emoji name without `:` - defaults to star emoji")] string? emoji = null, [Summary("threshold", "Amount of staremoji to trigger the service"), MinValue(1), MaxValue(int.MaxValue)] int amount = 3)
    {
        _starboardService.Configure(Context.Guild.Id, channel?.Id ?? Context.Channel.Id, emoji, amount);

        await RespondAsync($"Starred messages will now appear in <#{channel?.Id ?? Context.Channel.Id}>");
    }
}
