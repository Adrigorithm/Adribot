using Adribot.src.constants.enums;
using System;

namespace Adribot.src.extensions;

public static class TimeSpanTypeExtensions
{
    public static DateTimeOffset ToEndDate(this TimeSpanType timeSpan, int factor, DateTimeOffset now) => timeSpan switch
    {
        TimeSpanType.SECONDS => now.AddSeconds(Convert.ToDouble(factor)),
        TimeSpanType.MINUTES => now.AddMinutes(Convert.ToDouble(factor)),
        TimeSpanType.HOURS => now.AddHours(Convert.ToDouble(factor)),
        TimeSpanType.DAYS => now.AddDays(Convert.ToDouble(factor)),
        TimeSpanType.WEEKS => now.AddDays(Convert.ToDouble(factor) * 7),
        TimeSpanType.MONTHS => now.AddMonths(Convert.ToInt16(factor)),
        TimeSpanType.YEARS => now.AddYears(Convert.ToInt16(factor)),
        _ => throw new ArgumentException($"Invalid argument: Value [{timeSpan}] from TimeSpanType enum doesn't exist.")
    };
}
