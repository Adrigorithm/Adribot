using Adribot.constants.enums.recipe;

namespace Adribot.entities.fun.recipe;

public class RecipeIngredient
{
    public int RecipeIngredientId { get; set; }

    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }

    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    public float Quantity { get; set; }
    public IngredientUnit Unit { get; set; } = IngredientUnit.Gramme;
    public bool Optional { get; set; }

    public void ToHumanReadable()
    {
        switch (Quantity, Unit)
        {
            case (>= 1000, IngredientUnit.Gramme):
                Quantity /= 1000;
                Unit = IngredientUnit.Kilogramme;

                break;
            case (< 1000, IngredientUnit.Kilogramme):
                Quantity *= 1000;
                Unit = IngredientUnit.Gramme;

                break;
        }
    }

    public RecipeIngredient Clone() =>
        new()
        {
            RecipeIngredientId = RecipeIngredientId,
            IngredientId = IngredientId,
            Ingredient = Ingredient,
            RecipeId = RecipeId,
            Quantity = Quantity,
            Unit = Unit,
            Optional = Optional
        };
}
