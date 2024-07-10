using System.Threading.Tasks;
using Adribot.src.services;
using Discord;
using Discord.Interactions;

namespace Adribot.src.commands.utilities;

public class StarboardCommands(StarboardService _starboardService) : InteractionModuleBase
{
    [SlashCommand("starboard", "Configure the starboard service")]
    [RequireUserPermission(GuildPermission.Administrator)]
    [RequireContext(ContextType.Guild)]
    public async Task ConfigureAsync(InteractionContext ctx, [Summary("channel", "defaults to current channel")] ITextChannel? channel = null, [Summary("emoji", "emoji name without `:` - defaults to star emoji")] string? emoji = null, [Summary("threshold", "Amount of staremoji to trigger the service"), MinValue(1), MaxValue(int.MaxValue)] int amount = 3)
    {
        _starboardService.Configure(ctx.Guild.Id, channel?.Id ?? ctx.Channel.Id, emoji, amount);

        await RespondAsync($"Starred messages will now appear in <#{channel?.Id ?? ctx.Channel.Id}>");
    }
}
