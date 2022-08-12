using System;

public record Infraction{
    public int InfractionId {get; set;}
    public DateTime Date {get; set;}
    public TimeSpan Duration {get; set;}
    public InfractionType Type {get; set;}

    public ulong UserId {get; set;}
    public DUser DUser {get; set;}
}