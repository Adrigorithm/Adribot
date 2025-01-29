using System.Collections.Generic;
using System.Linq;
using Discord;

namespace Adribot.Helpers.validators;

public static class EmoteValidator
{
    public static (bool isValid, string? error) ValidateEmote(this string rawEmoteString, out List<Emote> emotes)
    {
        emotes = new List<Emote>();
        var rawEmotes = rawEmoteString.Split(' ');

        for (var i = 0; i < rawEmotes.Length; i++)
        {
            var isEmote = Emote.TryParse(rawEmotes[i], out Emote? parsedEmote);

            if (!isEmote)
                return (false, $"Problem parsing {OrdinalNumberalStringifier.Short(i + 1)} emote. No emotes were added.");
            
            emotes.Add(parsedEmote);
        }
        
        return (true, null);
    }
    
    public static (bool isValid, string? error) ValidateEmoji(this string rawEmojiString, out List<Emoji> emojis)
    {
        emojis = new List<Emoji>();
        var rawEmojis = rawEmojiString.Split(' ');

        for (var i = 0; i < rawEmojis.Length; i++)
        {
            var isEmoji = Emoji.TryParse(rawEmojis[i], out Emoji? parsedEmoji);

            if (!isEmoji)
                return (false, $"Problem parsing {OrdinalNumberalStringifier.Short(i + 1)} emoji. No emojis were added.");
            
            emojis.Add(parsedEmoji);
        }
        
        return (true, null);
    }
}
