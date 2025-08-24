using System.Collections.Generic;
using System.Threading.Tasks;
using Adribot.helpers.validators;
using Adribot.services;
using Discord;
using Discord.Interactions;

namespace Adribot.commands.utilities;

public class StarboardCommands(StarboardService starboardService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("starboard", "Configure the starboard service")]
    [RequireUserPermission(GuildPermission.Administrator)]
    [RequireContext(ContextType.Guild)]
    public async Task ConfigureAsync([Summary("channel", "defaults to current channel")] ITextChannel? channel = null, [Summary("emojis", "list of emojis to track, separated by spaces")] string? emojis = null, [Summary("emotes", "list of emotes to track, separated by spaces")] string? emotes = null, [Summary("threshold", "Amount of staremoji to trigger the service")][MinValue(1)][MaxValue(int.MaxValue)] int amount = 3)
    {
        var emotesList = new List<string>();
        var emojisList = new List<string>();

        if (emotes is not null)
        {
            emotes = emotes.Trim();

            (bool isValid, string error) isValid = emotes.ValidateEmote(out emotesList);

            if (!isValid.isValid)
            {
                await RespondAsync($"Could not parse emotes: {isValid.error}", ephemeral: true);
                return;
            }
        }

        if (emojis is not null)
        {
            emojis = emojis.Trim();

            (bool isValid, string error) isValid = emojis.ValidateEmoji(out emojisList);

            if (!isValid.isValid)
            {
                await RespondAsync($"Could not parse emojis: {isValid.error}", ephemeral: true);
                return;
            }
        }

        if (emotesList.Count == 0 && emojisList.Count == 0)
        {
            await RespondAsync("No emotes found", ephemeral: true);
            return;
        }

        starboardService.Configure(Context.Guild.Id, channel?.Id ?? Context.Channel.Id, emotesList, emojisList, amount);

        await RespondAsync($"Starred messages will now appear in <#{channel?.Id ?? Context.Channel.Id}>");
    }
}
