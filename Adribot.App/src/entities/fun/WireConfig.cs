using Adribot.Entities.Discord;
using Discord;

namespace Adribot.Entities.fun;

public class WireConfig
{
    public int WireConfigId { get; set; }
    
    public string EmoteName { get; set; }
    public byte[] EmoteData { get; set; }
    
    public int DGuildId { get; set; }
    public DGuild DGuild { get; set; }
}
