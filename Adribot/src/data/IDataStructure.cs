using DSharpPlus.Entities;

namespace Adribot.src.data;

public interface IDataStructure
{
    DiscordEmbedBuilder GenerateEmbedBuilder();
}
