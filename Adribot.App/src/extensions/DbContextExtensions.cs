using Adribot.Constants.Enums.Recipe;
using Adribot.Data;
using Adribot.Entities.Fun.Recipe;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Adribot.Extensions;

public static class DbContextExtensions
{
    public static async Task SeedIfEmptyAsync(this AdribotContext context)
    {
        if (await context.Recipes.AnyAsync())
            return;

        // Ingredients

        var ingredient1 = new Ingredient {
            IngredientId = 1,
            Name = "dark chocolate"
        };

        var ingredient2 = new Ingredient {
            IngredientId = 2,
            Name = "coconut oil"
        };

        var ingredient3 = new Ingredient {
            IngredientId = 3,
            Name = "icing sugar"
        };

        var ingredient4 = new Ingredient {
            IngredientId = 4,
            Name = "all-purpose flour"
        };

        var ingredient5 = new Ingredient {
            IngredientId = 5,
            Name = "eggs"
        };

        var ingredient6 = new Ingredient {
            IngredientId = 6,
            Name = "dark chocolate chips"
        };

        var ingredient7 = new Ingredient {
            IngredientId = 7,
            Name = "cane sugar"
        };

        var ingredient8 = new Ingredient {
            IngredientId = 8,
            Name = "rock sugar"
        };

        var ingredient9 = new Ingredient {
            IngredientId = 9,
            Name = "oatmeal (fine)"
        };

        var ingredient10 = new Ingredient {
            IngredientId = 10,
            Name = "coconut milk"
        };

        var ingredient11 = new Ingredient {
            IngredientId = 11,
            Name = "baking soda"
        };

        var ingredient12 = new Ingredient {
            IngredientId = 12,
            Name = "salt"
        };

        var ingredient13 = new Ingredient {
            IngredientId = 13,
            Name = "vanilla essence"
        };

        var ingredient14 = new Ingredient {
            IngredientId = 14,
            Name = "cinnamon ground"
        };

        var ingredient15 = new Ingredient {
            IngredientId = 15,
            Name = "ginger ground"
        };

        var ingredient16 = new Ingredient {
            IngredientId = 16,
            Name = "nutmeg ground"
        };

        var ingredient17 = new Ingredient {
            IngredientId = 17,
            Name = "ground cloves"
        };

        var ingredient18 = new Ingredient {
            IngredientId = 18,
            Name = "baking powder"
        };

        // RecipeIngredients

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        // TODO: continue here
        var recipeIngredient3 = new RecipeIngredient {
            RecipeIngredientId = 3,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient1 = new RecipeIngredient {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        // Recipes

        var recipe0 = new Recipe
        {
            Name = "Chocolate lava cakes",
            ImageUri = "/someuri/",
            Servings = 12,
            Difficulty = 0,
            Duration = 900,
            OvenMode = OvenMode.Fan,
            RecipeId = 1,
            Temperature = 453.15F
            Instruction = [
                "Melt the chocolate (not the pieces) combined with the coconut oil. Using a microwave, take it out every 30 seconds and stir well until done to prevent it from overheating the chocolate.",
                "Add icing sugar and stir until homogeneous. Repeat this process with the flour.",
                "Add the eggs one after the other, stir well in between.",
                "Evenly distribute the mixture over about 12 cupcake tins (I have no idea what to call them, look at the picture). When this is done gently push a piece of chocolate in each cake until it is partly covered and just not hitting the bottom.",
                "Bake. Allow to cool for three to five minutes before eating.",
                "They can remain in room temperature for up to three days. Heaten for fifteen seconds @ 600W in a microwave."
            ],
            RecipeIngredients = []
        };

        context.AddRange(
            recipe0
        );

        await context.SaveChangesAsync();
    }
}
