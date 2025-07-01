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
    public bool Optional { get; set; }

    public RecipeIngredient ToHumanReadable()
    {
        var recipeIngredient = new RecipeIngredient
        {
            RecipeIngredientId = RecipeIngredientId,
            IngredientId = IngredientId,
            Optional = Optional,
            Ingredient = Ingredient,
            Recipe = Recipe,
            RecipeId = RecipeId,
        };

        if (Quantity < 1000)
        {
            recipeIngredient.Quantity = Quantity;
            recipeIngredient.Unit = Unit;

            return recipeIngredient;
        }

        recipeIngredient.Quantity = Quantity / 1000;
        recipeIngredient.Unit = IngredientUnit.Kilogramme;

        return recipeIngredient;
    }
}
