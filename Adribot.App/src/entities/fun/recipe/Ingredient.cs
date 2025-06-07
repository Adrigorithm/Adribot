using Adribot.Constants.Enums.Recipe;

namespace Adribot.Entities.Fun.Recipe;

public class Ingredient
{
    public int IngredientId { get; set; }

    public string Name { get; set; }
    public float Quantity { get; set; }
    public IngredientUnit Unit { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}
