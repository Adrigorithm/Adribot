using Adribot.Constants.Enums.Recipe;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Entities.Fun.Recipe;

[PrimaryKey(nameof(IngredientId), nameof(RecipeId))]
public class RecipeIngredient
{
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    public float Quantity { get; set; }
    public IngredientUnit Unit { get; set; }
}
