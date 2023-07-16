using FoodApp.Models;
using FoodApp.Models.Dtos;
using FoodApp.Models.Models;
using FoodApp.Repository.Implementation;
using FoodApp.Repository.Interface;
using FoodApp.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FoodApp.Service.Implementation
{
    public class RecipeService : IRecipeServive
    {
        private readonly IRecipeRepository recipeRepository;
        private readonly IIngredientRepository ingredientRepository;
        private readonly IUserRepository userRepository;

        private readonly IFavoriteRecipeUsersRepository favoriteRecipeUsersRepository;
        public RecipeService(IRecipeRepository recipeRepository, 
            IFavoriteRecipeUsersRepository favoriteRecipeUsersRepository, 
            IIngredientRepository ingredientRepository,
            IUserRepository userRepository)
        {
            this.recipeRepository = recipeRepository;
            this.favoriteRecipeUsersRepository = favoriteRecipeUsersRepository;
            this.ingredientRepository = ingredientRepository;
            this.userRepository = userRepository;

        }

        public bool Add(RecipeViewModel model, string userId)
        {            
                // Create a new Recipe instance and save it
                var recipe = new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = model.RecipeTitle,
                    PreparationDescription = model.PreparationDescription,
                    Category = model.SelectedCategory,
                    Ingridients = new List<Ingredient>(),
                    FavoriteRecipes = new List<FavoriteRecipeUser>()

                };

            recipeRepository.Add(recipe);

            // Convert the IngredientViewModels to Ingredients and add them to the Recipe
            if (model.Ingredients != null)
                {
                    foreach (var ingredientViewModel in model.Ingredients)
                    {
                        var ingredient = new Ingredient
                        {
                            Id = Guid.NewGuid(),
                            Name = ingredientViewModel.Name,
                            Quantity = ingredientViewModel.Quantity,
                            UnitOfMeasurement = ingredientViewModel.SelectedUnit,
                            RecipeId = recipe.Id
                        };
                    //recipe.Ingridients.Add(ingredient);

                    ingredientRepository.Add(ingredient);
                    }
                }


                recipe.OwnerOfRecipeId = userId;

            return true;
        }

        public void AddRecipeToFavorites(Guid recipeId, string userId)
        {
            var user = userRepository.Get(userId);

            Recipe recipe = recipeRepository.GetById(recipeId);
          

            FavoriteRecipeUser item = new FavoriteRecipeUser
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                User = user,
                RecipeId = recipeId,
                Recipe = recipe
            };

            favoriteRecipeUsersRepository.Add(item);
        }

        public void Delete(Guid id)
        {
            recipeRepository.Delete(id);
        }

        public RecipeViewModel EditGet(Guid id, string userId)
        {
            Recipe recipe = recipeRepository.GetById(id);
              
            List<IngredientViewModel> ingredients = new List<IngredientViewModel>();


            foreach (var ingredient in recipe.Ingridients)
            {
                ingredients.Add(new IngredientViewModel
                {
                    IngredientId = ingredient.Id,
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity,
                    SelectedUnit = ingredient.UnitOfMeasurement
                });

                //ingredientRepository.Update(ingredient);

            }

            var viewModel = new RecipeViewModel
            {
                RecipeId = id,
                RecipeTitle = recipe.Title,
                PreparationDescription = recipe.PreparationDescription,
                SelectedCategory = recipe.Category,
                Ingredients = ingredients
            };

            //recipeRepository.Update(recipe);
            return viewModel;
        }

        public void EditPost(RecipeViewModel model)
        {

            Recipe recipe = recipeRepository.GetById(model.RecipeId);


                recipe.Title = model.RecipeTitle;
                recipe.PreparationDescription = model.PreparationDescription;
                recipe.Category = model.SelectedCategory;

                if (model.Ingredients != null)
                {
                    foreach (var ingredientViewModel in model.Ingredients)
                    {
                    Ingredient ingredient = ingredientRepository.GetById(ingredientViewModel.IngredientId);


                        ingredient.Name = ingredientViewModel.Name;
                        ingredient.Quantity = ingredientViewModel.Quantity;
                        ingredient.UnitOfMeasurement = ingredientViewModel.SelectedUnit;

                    ingredientRepository.Update(ingredient);
                    }
                }

                recipeRepository.Update(recipe);
                


        }

        public List<Recipe> getAllRecipes()
        {
            return recipeRepository.GetAllWithNullCookingClass();
        }

        public List<RecipeDto> getAllRecipesDto(string userId)
        {
            List<Recipe> recipes = recipeRepository.GetAll();

            List<RecipeDto> recipeDtos = new List<RecipeDto>();
            foreach (var r in recipes)
            {
                List<string> favRecipeOnUsers = favoriteRecipeUsersRepository.GetFavoriteRecipeUsers()
                    .Where(f => f.RecipeId == r.Id)
                    .Select(f => f.UserId).ToList();
                recipeDtos.Add(new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    PreparationDescription = r.PreparationDescription,
                    Ingridients = r.Ingridients,
                    OwnerOfRecipe = r.OwnerOfRecipe,
                    OwnerOfRecipeId = r.OwnerOfRecipeId,
                    FavoriteRecipes = r.FavoriteRecipes,
                    Category = r.Category,
                    IsFavorite = favRecipeOnUsers.Contains(userId) ? true : false,
                });

            }

            return recipeDtos;
        }

        public List<Recipe> getAllRecipesWithNullCookingClass()
        {
            return this.recipeRepository.GetAllWithNullCookingClass();
        }

        public void RemoveRecipeFromFavorites(Guid recipeId, string userId)
        {
            var user = userRepository.Get(userId);

            Recipe recipe = recipeRepository.GetById(recipeId);

            FavoriteRecipeUser favoriteRecipeUser = favoriteRecipeUsersRepository.GetByIdAndUser(recipeId, userId);

            favoriteRecipeUsersRepository.Delete(favoriteRecipeUser);
        }
    }
}
