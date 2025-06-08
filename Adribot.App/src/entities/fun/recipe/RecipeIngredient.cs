using Adribot.Constants.Enums.Recipe;

namespace Adribot.Entities.Fun.Recipe;

public class RecipeIngredient
{
    public int RecipeIngredientId { get; set; }

    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    public float Quantity { get; set; }
    public IngredientUnit Unit { get; set; }
}
