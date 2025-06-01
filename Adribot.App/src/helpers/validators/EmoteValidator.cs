using System.Collections.Generic;
using Discord;

namespace Adribot.Helpers.validators;

public static class EmoteValidator
{
    public static (bool isValid, string? error) ValidateEmote(this string rawEmoteString, out List<string> emotes)
    {
        emotes = [];
        var rawEmotes = rawEmoteString.Split(' ');

        for (var i = 0; i < rawEmotes.Length; i++)
        {
            var isEmote = Emote.TryParse(rawEmotes[i], out _);

            if (!isEmote)
                return (false, $"Problem parsing {OrdinalNumberalStringifier.Short(i + 1)} emote. No emotes were added.");

            emotes.Add(rawEmotes[i]);
        }

        return (true, null);
    }

    public static (bool isValid, string? error) ValidateEmoji(this string rawEmojiString, out List<string> emojis)
    {
        emojis = [];
        var rawEmojis = rawEmojiString.Split(' ');

        for (var i = 0; i < rawEmojis.Length; i++)
        {
            var isEmoji = Emoji.TryParse(rawEmojis[i], out _);

            if (!isEmoji)
                return (false, $"Problem parsing {OrdinalNumberalStringifier.Short(i + 1)} emoji. No emojis were added.");

            emojis.Add(rawEmojis[i]);
        }

        return (true, null);
    }
}
