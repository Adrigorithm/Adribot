using System.Collections.Generic;
using Adribot.Constants.Enums.Recipe;

namespace Adribot.Entities.Fun.Recipe;

public class Recipe
{
    public int RecipeId { get; set; }

    public string Name { get; set; }
    public string ImageUri { get; set; }
    public OvenSetting OvenSetting { get; set; }
    public short Servings { get; set; }
    public string Instruction { get; set; }
    public byte Difficulty { get; set; }

    // Oven
    public OvenMode OvenMode { get; set; }
    public short Temperature { get; set; } // SI (K) in database
    public short Duration { get; set; } // SI (s) in database

    public List<Ingredient> Ingredients { get; set; }
}
