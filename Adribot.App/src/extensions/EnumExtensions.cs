using System;
using Adribot.constants.enums;
using Adribot.constants.enums.recipe;

namespace Adribot.extensions;

public static class EnumExtensions
{
    public static string ToSymbol(this IngredientUnit unit) =>
        unit switch
        {
            IngredientUnit.Gramme => "g",
            IngredientUnit.Kilogramme => "kg",
            IngredientUnit.Piece => "p",
            IngredientUnit.Teaspoon => "tsp",
            _ => throw new NotImplementedException()
        };

    public static string ToSymbol(this Units unit) =>
        unit switch
        {
            Units.Imperial => "°F",
            Units.Metric => "°C",
            Units.Si => "K",
            _ => throw new NotImplementedException()
        };

    public static string ToHumanReadable(this OvenMode mode) =>
        mode switch
        {
            OvenMode.Fan => "Convection",
            _ => throw new NotImplementedException()
        };
}
