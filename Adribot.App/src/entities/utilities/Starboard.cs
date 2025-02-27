using System;
using System.Collections.Generic;
using Adribot.Data;
using Adribot.Entities.Discord;
using Discord;

namespace Adribot.Entities.Utilities;

public class Starboard : IDataStructure
{
    public int StarboardId { get; set; }

    public ulong? Channel { get; set; }
    public int Threshold { get; set; }
    
    public List<string>? StarEmotes { get; set; }
    public List<string>? StarEmojis { get; set; }

    public List<DMessage> Messages { get; set; } = [];

    public int DGuildId { get; set; }
    public DGuild DGuild { get; set; }

    public EmbedBuilder GenerateEmbedBuilder() => throw new System.NotImplementedException();
}
