using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public record DGuild{
    [Key]
    public ulong GuildId {get; set;}

    public List<DUser> Users {get; set;} = new();
}