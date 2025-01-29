using System.Collections.Generic;
using System.Threading.Tasks;
using Adribot.Helpers.validators;
using Adribot.Services;
using Discord;
using Discord.Interactions;
using Emote = Discord.Emote;

namespace Adribot.Commands.Utilities;

public class StarboardCommands(StarboardService starboardService) : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("starboard", "Configure the starboard service")]
    [RequireUserPermission(GuildPermission.Administrator)]
    [RequireContext(ContextType.Guild)]
    public async Task ConfigureAsync([Summary("channel", "defaults to current channel")] ITextChannel? channel = null, [Summary("emojis", "list of emojis to track, separated by spaces")] string? emojis = null, [Summary("emotes", "list of emotes to track, separated by spaces")] string? emotes = null, [Summary("threshold", "Amount of staremoji to trigger the service"), MinValue(1), MaxValue(int.MaxValue)] int amount = 3)
    {
        var emoteList = new List<IEmote>();

        if (emotes is not null)
        {
            var emotesParsed = new List<Emote>();
            (bool isValid, string error) isValid = EmoteValidator.ValidateEmote(emotes, out emotesParsed);

            if (!isValid.isValid)
            {
                await RespondAsync($"Could not parse emotes: {isValid.error}");
                return;
            }

            emoteList.AddRange(emotesParsed);
        }
        
        if (emojis is not null)
        {
            var emojisParsed = new List<Emoji>();
            (bool isValid, string error) isValid = EmoteValidator.ValidateEmoji(emojis, out emojisParsed);
            
            if (!isValid.isValid)
            {
                await RespondAsync($"Could not parse emojis: {isValid.error}");
                return;
            }
            
            emoteList.AddRange(emojisParsed);
        }
        
        
        starboardService.Configure(Context.Guild.Id, channel?.Id ?? Context.Channel.Id, emoteList, amount);

        await RespondAsync($"Starred messages will now appear in <#{channel?.Id ?? Context.Channel.Id}>");
    }
}
