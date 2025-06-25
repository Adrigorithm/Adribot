using System;
using System.Threading.Tasks;
using Adribot.Constants.Enums.Recipe;
using Adribot.Data;
using Adribot.Entities.Fun.Recipe;
using Microsoft.EntityFrameworkCore;

namespace Adribot.Extensions;

public static class DbContextExtensions
{
    public static async Task SeedIfEmptyAsync(this AdribotContext context)
    {
        if (await context.Recipes.AnyAsync())
            return;

        // Ingredients

        var ingredient1 = new Ingredient
        {
            IngredientId = 1,
            Name = "dark chocolate"
        };

        var ingredient2 = new Ingredient
        {
            IngredientId = 2,
            Name = "coconut oil"
        };

        var ingredient3 = new Ingredient
        {
            IngredientId = 3,
            Name = "icing sugar"
        };

        var ingredient4 = new Ingredient
        {
            IngredientId = 4,
            Name = "all-purpose flour"
        };

        var ingredient5 = new Ingredient
        {
            IngredientId = 5,
            Name = "eggs"
        };

        var ingredient6 = new Ingredient
        {
            IngredientId = 6,
            Name = "dark chocolate chips"
        };

        var ingredient7 = new Ingredient
        {
            IngredientId = 7,
            Name = "cane sugar"
        };

        var ingredient8 = new Ingredient
        {
            IngredientId = 8,
            Name = "rock sugar"
        };

        var ingredient9 = new Ingredient
        {
            IngredientId = 9,
            Name = "oatmeal (fine)"
        };

        var ingredient10 = new Ingredient
        {
            IngredientId = 10,
            Name = "coconut milk"
        };

        var ingredient11 = new Ingredient
        {
            IngredientId = 11,
            Name = "baking soda"
        };

        var ingredient12 = new Ingredient
        {
            IngredientId = 12,
            Name = "salt"
        };

        var ingredient13 = new Ingredient
        {
            IngredientId = 13,
            Name = "vanilla essence"
        };

        var ingredient14 = new Ingredient
        {
            IngredientId = 14,
            Name = "cinnamon ground"
        };

        var ingredient15 = new Ingredient
        {
            IngredientId = 15,
            Name = "ginger ground"
        };

        var ingredient16 = new Ingredient
        {
            IngredientId = 16,
            Name = "nutmeg ground"
        };

        var ingredient17 = new Ingredient
        {
            IngredientId = 17,
            Name = "ground cloves"
        };

        var ingredient18 = new Ingredient
        {
            IngredientId = 18,
            Name = "baking powder"
        };

        var ingredient19 = new Ingredient
        {
            IngredientId = 19, 
            Name = "Cupcake tin"
        };

        // RecipeIngredients

        var recipeIngredient1 = new RecipeIngredient
        {
            RecipeIngredientId = 1,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 300,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient2 = new RecipeIngredient
        {
            RecipeIngredientId = 2,
            IngredientId = 1,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient3 = new RecipeIngredient
        {
            RecipeIngredientId = 3,
            IngredientId = 2,
            RecipeId = 1,
            Quantity = 120,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient4 = new RecipeIngredient
        {
            RecipeIngredientId = 4,
            IngredientId = 3,
            RecipeId = 1,
            Quantity = 100,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient5 = new RecipeIngredient
        {
            RecipeIngredientId = 5,
            IngredientId = 4,
            RecipeId = 1,
            Quantity = 120,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient6 = new RecipeIngredient
        {
            RecipeIngredientId = 6,
            IngredientId = 5,
            RecipeId = 1,
            Quantity = 4,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient7 = new RecipeIngredient
        {
            RecipeIngredientId = 7,
            IngredientId = 6,
            RecipeId = 2,
            Quantity = 200,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient8 = new RecipeIngredient
        {
            RecipeIngredientId = 8,
            IngredientId = 7,
            RecipeId = 2,
            Quantity = 80,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient9 = new RecipeIngredient
        {
            RecipeIngredientId = 9,
            IngredientId = 8,
            RecipeId = 2,
            Quantity = 80,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient10 = new RecipeIngredient
        {
            RecipeIngredientId = 10,
            IngredientId = 2,
            RecipeId = 2,
            Quantity = 60,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient11 = new RecipeIngredient
        {
            RecipeIngredientId = 11,
            IngredientId = 9,
            RecipeId = 2,
            Quantity = 225,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient12 = new RecipeIngredient
        {
            RecipeIngredientId = 12,
            IngredientId = 10,
            RecipeId = 2,
            Quantity = 120,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient13 = new RecipeIngredient
        {
            RecipeIngredientId = 13,
            IngredientId = 5,
            RecipeId = 2,
            Quantity = 1,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient14 = new RecipeIngredient
        {
            RecipeIngredientId = 14,
            IngredientId = 11,
            RecipeId = 2,
            Quantity = .5F,
            Unit = IngredientUnit.Teaspoon
        };

        var recipeIngredient15 = new RecipeIngredient
        {
            RecipeIngredientId = 15,
            IngredientId = 12,
            RecipeId = 2,
            Quantity = .25F,
            Unit = IngredientUnit.Teaspoon
        };

        var recipeIngredient16 = new RecipeIngredient
        {
            RecipeIngredientId = 16,
            IngredientId = 13,
            RecipeId = 2,
            Quantity = 2,
            Unit = IngredientUnit.Teaspoon,
            Optional = true
        };

        var recipeIngredient17 = new RecipeIngredient
        {
            RecipeIngredientId = 17,
            IngredientId = 8,
            RecipeId = 3,
            Quantity = 200,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient18 = new RecipeIngredient
        {
            RecipeIngredientId = 18,
            IngredientId = 2,
            RecipeId = 3,
            Quantity = 160,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient19 = new RecipeIngredient
        {
            RecipeIngredientId = 19,
            IngredientId = 5,
            RecipeId = 3,
            Quantity = 2,
            Unit = IngredientUnit.Piece
        };

        var recipeIngredient20 = new RecipeIngredient
        {
            RecipeIngredientId = 20,
            IngredientId = 4,
            RecipeId = 3,
            Quantity = 500,
            Unit = IngredientUnit.Gramme
        };

        var recipeIngredient21 = new RecipeIngredient
        {
            RecipeIngredientId = 21,
            IngredientId = 18,
            RecipeId = 3,
            Quantity = 1.5F,
            Unit = IngredientUnit.Teaspoon
        };

        var recipeIngredient22 = new RecipeIngredient
        {
            RecipeIngredientId = 22,
            IngredientId = 14,
            RecipeId = 3,
            Quantity = 2,
            Unit = IngredientUnit.Teaspoon
        };

        var recipeIngredient23 = new RecipeIngredient
        {
            RecipeIngredientId = 23,
            IngredientId = 15,
            RecipeId = 3,
            Quantity = .5F,
            Unit = IngredientUnit.Teaspoon
        };

        var recipeIngredient24 = new RecipeIngredient
        {
            RecipeIngredientId = 24,
            IngredientId = 16,
            RecipeId = 3,
            Quantity = .5F,
            Unit = IngredientUnit.Teaspoon
        };

        var recipeIngredient25 = new RecipeIngredient
        {
            RecipeIngredientId = 25,
            IngredientId = 17,
            RecipeId = 3,
            Quantity = .5F,
            Unit = IngredientUnit.Teaspoon
        };

        var recipeIngredient26 = new RecipeIngredient
        {
            RecipeIngredientId = 26,
            IngredientId = 19,
            RecipeId = 1,
            Quantity = 12,
            Unit = IngredientUnit.Piece
        };

        // Recipes

        var recipe1 = new Recipe
        {
            Name = "Chocolate lava cakes",
            ImageUri = "/someuri/",
            Servings = 12,
            Difficulty = 0,
            Duration = 900,
            OvenMode = OvenMode.Fan,
            RecipeId = 1,
            Temperature = 453.15F,
            Instruction = [
                "Melt the chocolate (not the pieces) combined with the coconut oil. Using a microwave, take it out every 30 seconds and stir well until done to prevent it from overheating the chocolate.",
                "Add icing sugar and stir until homogeneous. Repeat this process with the flour.",
                "Add the eggs one after the other, stir well in between.",
                "Evenly distribute the mixture over the cupcake tins (I have no idea what to call them, look at the picture). When this is done gently push a piece of chocolate in each cake.",
                "Bake. Allow to cool for three to five minutes before eating.",
                "They can remain in room temperature for up to three days. Heaten one for fifteen seconds @ 600W in a microwave."
            ]
        };

        var recipe2 = new Recipe
        {
            Name = "Chocolate chip cookies",
            ImageUri = "/someuri/",
            Servings = 10,
            Difficulty = 1,
            Duration = 1500,
            OvenMode = OvenMode.Fan,
            RecipeId = 2,
            Temperature = 443.15F,
            Instruction = [
                "Make sure you have all ingredients at the ready, as minimal delay should occur between all stages.",
                "Combine all the sugar and coconut oil (melt if solid). Add the eggs, salt, baking soda and vanilla essence.",
                "Add the oatmeal and coconut milk (if you used oatmeal, half the amount of milk will suffice) and stir until homogeneous. Lastly add the chocolate chips and combine",
                "Put a sheet of baking paper on one oven tin (make sure it covers the whole plate). Pour the mixture on it and divide evenly over the plate as a thin in layer. You may need additional plates if you adjusted the serving amount.",
                "Bake. Allow to cool for ten minutes and cut into cookies of desired size with a sharp knife or pizza cutter",
                "They can be preserved in room temperature for up to five days."
            ]
        };

        var recipe3 = new Recipe
        {
            Name = "Speculoos cookies",
            ImageUri = "/someuri/",
            Servings = 90,
            Difficulty = 1,
            Duration = 900,
            OvenMode = OvenMode.Fan,
            RecipeId = 3,
            Temperature = 443.15F,
            Instruction = [
                "Melt the coconut oil and stir it with the sugar until combined. Add the eggs one after the other, stir well in between.",
                "In another bowl, combine the flour, baking powder and all spices.",
                "Add a third of this mixture to the 'coconut oil and sugar concoction' and stir until homogeneous. Repeat this 2 times (for the remaining thirds).",
                "Shape the mixture to a ball and put it in the refrigerator encased in plastic foil for at least half an hour (or overnight).",
                "Roll out a chunk of dough to an even surface of three to five millimetres thick. Use cookie cutters of your choice to cut out shapes.",
                "They do rise very little so they can almost touch each other on the baking sheet, preserving space (use baking paper or something similar).",
                "Repeat steps until the baking sheet is full (you can bake multiple sheets at once).",
                "Enjoy! (You may need to repeat the 3 previous tasks depending on the amount of dough you have :) ). They can be kept in room temperature for a few days but if you baked many, the freezer is preferable."
            ]
        };
        
        await using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();

        try
        {
            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Ingredients ON");

            context.Ingredients.AddRange([
                ingredient1,
                ingredient2,
                ingredient3,
                ingredient4,
                ingredient5,
                ingredient6,
                ingredient7,
                ingredient8,
                ingredient9,
                ingredient10,
                ingredient11,
                ingredient12,
                ingredient13,
                ingredient14,
                ingredient15,
                ingredient16,
                ingredient17,
                ingredient18,
                ingredient19
            ]);
            await context.SaveChangesAsync();

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Ingredients OFF");

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Recipes ON");

            context.Recipes.AddRange([
                recipe1,
                recipe2,
                recipe3
            ]);
            await context.SaveChangesAsync();

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Recipes OFF");

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.RecipeIngredients ON");

            context.RecipeIngredients.AddRange([
                recipeIngredient1,
                recipeIngredient2,
                recipeIngredient3,
                recipeIngredient4,
                recipeIngredient5,
                recipeIngredient6,
                recipeIngredient7,
                recipeIngredient8,
                recipeIngredient9,
                recipeIngredient10,
                recipeIngredient11,
                recipeIngredient12,
                recipeIngredient13,
                recipeIngredient14,
                recipeIngredient15,
                recipeIngredient16,
                recipeIngredient17,
                recipeIngredient18,
                recipeIngredient19,
                recipeIngredient20,
                recipeIngredient21,
                recipeIngredient22,
                recipeIngredient23,
                recipeIngredient24,
                recipeIngredient25,
                recipeIngredient26
            ]);
            await context.SaveChangesAsync();

            await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.RecipeIngredients OFF");

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();

            throw;
        }

        // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Ingredients ON");
        //
        // context.Ingredients.AddRange([ingredient1, ingredient2, ingredient3, ingredient4, ingredient5, ingredient6, ingredient7, ingredient8, ingredient9, ingredient10, ingredient11, ingredient12, ingredient13, ingredient14, ingredient15, ingredient16, ingredient17, ingredient18]);
        // await context.SaveChangesAsync();
        //
        // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Ingredients OFF");
        //
        // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Recipes ON");
        //
        // context.Recipes.AddRange([recipe1, recipe2, recipe3]);
        // await context.SaveChangesAsync();
        //
        // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Recipes OFF");
        //
        // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.RecipeIngredients ON");
        //
        // context.RecipeIngredients.AddRange([recipeIngredient1, recipeIngredient2, recipeIngredient3, recipeIngredient4, recipeIngredient5, recipeIngredient6, recipeIngredient7, recipeIngredient8, recipeIngredient9, recipeIngredient10, recipeIngredient11, recipeIngredient12, recipeIngredient13, recipeIngredient14, recipeIngredient15, recipeIngredient16, recipeIngredient17, recipeIngredient18, recipeIngredient19, recipeIngredient20, recipeIngredient21, recipeIngredient22, recipeIngredient23, recipeIngredient24, recipeIngredient25]);
        // await context.SaveChangesAsync();
        //
        // context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.RecipeIngredients OFF");

    }
}
