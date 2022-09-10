using System;
using System.Collections.Generic;

public static class KeyValuePairExtensions{
    public static DateTimeOffset ToEndDate(this KeyValuePair<TimeSpanType, long> timeSpan, DateTimeOffset now){
        return timeSpan switch{
            (TimeSpanType.SECONDS, _) => now.AddSeconds(Convert.ToDouble(timeSpan.Value)),
            (TimeSpanType.MINUTES, _) => now.AddMinutes(Convert.ToDouble(timeSpan.Value)),
            (TimeSpanType.HOURS, _) => now.AddHours(Convert.ToDouble(timeSpan.Value)),
            (TimeSpanType.DAYS, _) => now.AddDays(Convert.ToDouble(timeSpan.Value)),
            (TimeSpanType.WEEKS, _) => now.AddDays(Convert.ToDouble(timeSpan.Value * 7)),
            (TimeSpanType.MONTHS, _) => now.AddMonths(Convert.ToInt16(timeSpan.Value)),
            (TimeSpanType.YEARS, _) => now.AddYears(Convert.ToInt16(timeSpan.Value)),
            _ => throw new ArgumentException($"Invalid argument: Value [{ timeSpan.Value }] from TimeSpanType enum doesn't exist.")
        };
    }
}