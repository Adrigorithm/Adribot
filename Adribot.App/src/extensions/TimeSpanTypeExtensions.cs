using System;
using Adribot.src.constants.enums;

namespace Adribot.src.extensions;

public static class TimeSpanTypeExtensions
{
    public static DateTimeOffset ToEndDate(this TimeSpanType timeSpan, int factor, DateTimeOffset now) => timeSpan switch
    {
        TimeSpanType.Seconds => now.AddSeconds(Convert.ToDouble(factor)),
        TimeSpanType.Minutes => now.AddMinutes(Convert.ToDouble(factor)),
        TimeSpanType.Hours => now.AddHours(Convert.ToDouble(factor)),
        TimeSpanType.Days => now.AddDays(Convert.ToDouble(factor)),
        TimeSpanType.Weeks => now.AddDays(Convert.ToDouble(factor) * 7),
        TimeSpanType.Months => now.AddMonths(Convert.ToInt16(factor)),
        TimeSpanType.Years => now.AddYears(Convert.ToInt16(factor)),
        _ => throw new ArgumentException($"Invalid argument: Value [{timeSpan}] from TimeSpanType enum doesn't exist.")
    };
}
