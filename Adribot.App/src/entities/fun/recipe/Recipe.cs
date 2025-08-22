using System.Collections.Generic;
using System.Linq;
using Adribot.constants.enums;
using Adribot.constants.enums.recipe;
using Adribot.extensions;

namespace Adribot.entities.fun.recipe;

public class Recipe
{
    private Units _units = Units.Si;
    public int RecipeId { get; set; }

    public string Name { get; set; }
    public string ImageUri { get; set; }
    public short Servings { get; set; }
    public string[] Instruction { get; set; }
    public byte Difficulty { get; set; }

    // Oven
    public OvenMode OvenMode { get; set; }

    /// <summary>
    ///     The temperate the oven should be set to.
    ///     SI (K) in database.
    /// </summary>
    public float Temperature { get; set; }

    /// <summary>
    ///     How long it should be baked in the oven for.
    ///     SI (s) in database.
    /// </summary>
    public short Duration { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; }

    public void ConvertNumerals(Units to)
    {
        Temperature = Temperature.Convert(Unit.Temperature, _units, to);
        _units = to;
    }

    public void ToHumanReadable() =>
        RecipeIngredients.ForEach(recipeIngredient => recipeIngredient.ToHumanReadable());

    public void ChangeServings(short newServings, bool toHumanReadable = false)
    {
        RecipeIngredients.ForEach(ri =>
        {
            ri.Quantity *= (float)newServings / Servings;

            if (toHumanReadable)
                ri.ToHumanReadable();
        });

        Servings = newServings;
    }

    public Recipe Clone() =>
        new()
        {
            RecipeId = RecipeId,
            Name = Name,
            ImageUri = ImageUri,
            Servings = Servings,
            Instruction = Instruction,
            Difficulty = Difficulty,
            OvenMode = OvenMode,
            Temperature = Temperature,
            Duration = Duration,
            RecipeIngredients = RecipeIngredients.Select(ri => ri.Clone()).ToList()
        };
}
