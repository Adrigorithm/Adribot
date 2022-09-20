using System;

public record Tag{
    public int TagId {get; set;}
    public string Name {get; set;}
    public string Content {get; set;}
    public DateTimeOffset Date {get; set;}

    public ulong MemberId {get; set;}
    public DMember DMember {get; set;}
}