using System.Collections.Generic;
using System.Linq;
using Discord;

namespace Adribot.Helpers.validators;

public static class EmoteValidator
{
    public static (bool isValid, string? error) Validate(this string rawEmoteString, out List<Emote> emotes)
    {
        emotes = new List<Emote>();
        var rawEmotes = rawEmoteString.Split(' ');

        for (var i = 0; i < rawEmotes.Length; i++)
        {
            var emote = Emote.TryParse(rawEmotes[i], out Emote? parsedEmote);

            if (!emote)
                return (false, $"Problem parsing {OrdinalNumberalStringifier.Short(i + 1)} emote. No emotes were added.");
            
            emotes.Add(parsedEmote);
        }
        
        return (true, null);
    }
}
