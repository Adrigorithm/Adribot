using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Adribot.Entities.Discord;
using Discord;
using Discord.WebSocket;

namespace Adribot.Extensions;

public static class DiscordObjectExtensions
{
    /// <summary>
    ///     Calling this method method individually will NOT reference to a DMembers in any way.
    /// </summary>
    /// <returns>The simplified DTO (DGuild) representation of a DiscordGuild</returns>
    public static DGuild ToDGuild(this SocketGuild guild) =>
        new()
        {
            GuildId = guild.Id
        };

    /// <summary>
    ///     Calling this method method individually will NOT reference to a DGuild in any way.
    /// </summary>
    /// <param name="member">The member to be converted</param>
    /// <returns>The simplified DTO (DMember) representation of a DiscordMember</returns>
    public static DMember ToDMember(this SocketGuildUser member, int dGuildId) =>
        new()
        {
            MemberId = member.Id, DGuildId = dGuildId, Mention = member.Mention
        };

    public static Color ToDiscordColour(this string colourString, int baseFormat = 16) =>
        string.IsNullOrWhiteSpace(colourString)
            ? throw new ArgumentException("Cannot parse colour from an empty string")
            : colourString.Length switch
            {
                6 => new Color(Convert.ToUInt32(colourString, baseFormat)),
                8 => new Color(Convert.ToUInt32(colourString[2..], baseFormat)),
                _ => throw new ArgumentException($"Cannot parse colour: {colourString}")
            };

    public static string ToString(this SocketApplicationCommand command)
        => $"Command: {command.Name} [{(command.IsGlobalCommand
            ? "Global"
            : "Guild")}]";

    public static string ToEmoteString(this IEnumerable<IEmote> emotes)
    {
        var emoteString = new StringBuilder();

        emotes.ToImmutableList().ForEach(e => emoteString.Append(e.ToString() + ' '));

        return emoteString.ToString()[..(emoteString.Length - 1)];
    }

    public static List<Emote> ToEmoteList(this string emoteString)
    {
        var emotes = emoteString.Split(' ');

        return emotes.ToList().ConvertAll(Emote.Parse);
    }

    public static List<Emoji> ToEmojiList(this string emojiString)
    {
        var emojis = emojiString.Split(' ');

        return emojis.ToList().ConvertAll(Emoji.Parse);
    }
}
