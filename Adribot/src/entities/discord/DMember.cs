using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public record DMember{
    [Key]
    public ulong MemberId {get; set;}

    public List<Infraction> Infractions {get; set;} = new();
    public List<Reminder> reminders {get; set;} = new();
    public List<Tag> tags {get; set;} = new();

    public ulong GuildId {get; set;}
    public DGuild DGuild {get; set;}
}