using System;

public record Reminder{
    public int ReminderId {get; set;}
    public DateTimeOffset Date {get; set;}
    public DateTimeOffset EndDate {get; set;}
    public string Content {get; set;}

    public ulong MemberId {get; set;}
    public DMember DMember {get; set;}
}