using System;
using Adribot.Constants.Enums.Recipe;

namespace Adribot.Extensions;

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
}
