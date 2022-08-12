using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public record DUser{
    [Key]
    public ulong UserId {get; set;}
    public string Nickname {get; set;}

    public List<Infraction> Infractions {get; set;} = new();

    public List<DGuild> Guilds {get; set;}
}