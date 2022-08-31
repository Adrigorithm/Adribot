using System;

public class Infraction{
    public int InfractionId {get; set;}
    public DateTime Date {get; set;}
    public TimeSpan Duration {get; set;}
    public InfractionType Type {get; set;}
    public bool isExpired {get; set;}

    public ulong MemberId {get; set;}
    public DMember DMember {get; set;}

    public ulong GuildId {get; set;}
    public DGuild DGuild {get; set;}
}