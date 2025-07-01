using System;
using Adribot.Constants.Enums;

namespace Adribot.Extensions;

public static class RawTypeExtensions
{
    public static float Convert(this float value, Unit unit, Units from, Units to)
    {
        switch (unit)
        {
            case Unit.Temperature:
                return (from, to) switch
                {
                    (Units.Si, Units.Metric) => value - 273.15F,
                    (Units.Si, Units.Imperial) => (value - 273.15F) * (9F / 5) + 32,
                    (Units.Si, _) => value,

                    (Units.Metric, Units.Si) => value + 273.15F,
                    (Units.Metric, Units.Imperial) => value * (9F / 5) + 32,
                    (Units.Metric, _) => value,

                    (Units.Imperial, Units.Si) => (value - 32) * (5F / 9) + 273.15F,
                    (Units.Imperial, Units.Metric) => (value - 32) * (5F / 9),
                    (Units.Imperial, _) => value,

                    _ => throw new NotImplementedException()
                };
            default:
                throw new NotImplementedException();
        }
    }
}
