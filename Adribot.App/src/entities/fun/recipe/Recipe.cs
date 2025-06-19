using System.Collections.Generic;
using Adribot.Constants.Enums;
using Adribot.Constants.Enums.Recipe;

namespace Adribot.Entities.Fun.Recipe;

public class Recipe
{
    public int RecipeId { get; set; }

    public string Name { get; set; }
    public string ImageUri { get; set; }
    public short Servings { get; set; }
    public string[] Instruction { get; set; }
    public byte Difficulty { get; set; }

    // Oven
    public OvenMode OvenMode { get; set; }
    /// <summary>
    /// The temperate the oven should be set to.
    /// SI (K) in database.
    /// </summary>
    public float Temperature { get; set; }
    /// <summary>
    /// How long it should be baked in the oven for.
    /// SI (s) in database.
    /// </summary>
    public short Duration { get; set; }

    public List<RecipeIngredient> RecipeIngredients { get; set; }

    private Units _units = Units.Si;

    public void ConvertNumerals(Units to)
    {
        
        
        _units = to;
    }
}
