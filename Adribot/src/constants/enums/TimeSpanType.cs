using DSharpPlus.SlashCommands;

public enum TimeSpanType{
    [ChoiceName("Seconds")]
    SECONDS,
    [ChoiceName("Minutes")]
    MINUTES,
    [ChoiceName("Hours")]
    HOURS,
    [ChoiceName("Days")]
    DAYS,
    [ChoiceName("Weeks")]
    WEEKS,
    [ChoiceName("Months")]
    MONTHS,
    [ChoiceName("Years")]
    YEARS
}