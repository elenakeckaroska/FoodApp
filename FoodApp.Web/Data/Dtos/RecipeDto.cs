using FoodApp.Web.Data.Identity;
using FoodApp.Web.Data.Models;
using System.Collections.Generic;
using System;

namespace FoodApp.Web.Data.Dtos
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
