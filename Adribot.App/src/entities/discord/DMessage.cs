using Adribot.Data;
using Adribot.Entities.Utilities;
using Discord;

namespace Adribot.Entities.Discord;

public class DMessage : IDataStructure
{
    public int DMessageId { get; set; }
    public ulong MessageId { get; set; }
    public string JumpUrl { get; set; }
    public string Content { get; set; }
    
    public int DMemberId { get; set; }
    public DMember DMember { get; set; }
    
    public int StarboardId { get; set; }
    public Starboard Starboard { get; set; }
    
    public EmbedBuilder GenerateEmbedBuilder() => throw new System.NotImplementedException();
}
