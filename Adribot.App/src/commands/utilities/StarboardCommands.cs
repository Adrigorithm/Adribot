using System.Threading.Tasks;
using Adribot.src.helpers;
using Adribot.src.services;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Adribot.src.commands.utilities;

public class StarboardCommands(StarboardService _starboardService) : ApplicationCommandModule
{
    [SlashCommand("starboard", "Configure the starboard service")]
    [RequirePermissionOrDev(135081249017430016, Permissions.Administrator)]
    public async Task ConfigureAsync(InteractionContext ctx, [Option("channel", "defaults to current channel")] DiscordChannel channel = null, [Option("emoji", "emoji name without `:` - defaults to star emoji")] string? emoji = null, [Option("threshold", "Amount of staremoji to trigger the service"), Minimum(1), Maximum(int.MaxValue)] long amount = 3)
    {
        _starboardService.Configure(ctx.Guild.Id, channel?.Id ?? ctx.Channel.Id, emoji, (int)amount);

        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(
            new DiscordMessageBuilder().WithContent($"Starred messages will now appear in {channel?.Mention ?? ctx.Channel.Mention}")).AsEphemeral());
    }
}
