using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public record DGuild{
    public ulong GuildId {get; set;}

    public List<DMember> Members {get; set;} = new();
}