
using System.Collections.Generic;
using System;
using FoodApp.Models.Models;
using FoodApp.Models.Identity;

namespace FoodApp.Models.Dtos
{
    public class RecipeDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string PreparationDescription { get; set; }

        public virtual ICollection<Ingredient> Ingridients { get; set; }

        public virtual FoodAppUser OwnerOfRecipe { get; set; }

        public string OwnerOfRecipeId { get; set; }

        public string Category { get; set; }

        public virtual ICollection<FavoriteRecipeUser> FavoriteRecipes { get; set; }

        public Boolean IsFavorite { get; set; }
    }
}
