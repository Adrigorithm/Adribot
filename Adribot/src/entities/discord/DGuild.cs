using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class DGuild{
    [Key]
    public ulong GuildId {get; set;}

    public List<DMember> Members {get; set;} = new();

    public List<Infraction> Infractions {get; set;} = new();
}