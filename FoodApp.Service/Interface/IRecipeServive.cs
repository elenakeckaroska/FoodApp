﻿using FoodApp.Models;
using FoodApp.Models.Dtos;
using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Service.Interface
{
    public interface IRecipeServive
    {
        public List<RecipeDto> getAllRecipesDto(string userId);
        public List<Recipe> getAllRecipesWithNullCookingClass();

        public bool Add(RecipeViewModel model, string userId);

        public RecipeViewModel EditGet(Guid id, string userId);
        public void EditPost(RecipeViewModel model);

        public void Delete(Guid id);

        public void AddRecipeToFavorites(Guid recipeId, string userId);

        public void RemoveRecipeFromFavorites(Guid recipeId, string userId);
    }
}
