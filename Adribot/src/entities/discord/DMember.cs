using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class DMember{
    [Key]
    public ulong MemberId {get; set;}

    public List<Infraction> Infractions {get; set;} = new();

    public ulong GuildId {get; set;}
    public DGuild DGuild {get; set;}
}