using FoodApp.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoodApp.Models.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string PreparationDescription { get; set; }
        public string Category { get; set; }

        [JsonIgnore]

        public virtual ICollection<Ingredient> Ingridients { get; set; }

        [JsonIgnore]

        public virtual FoodAppUser OwnerOfRecipe { get; set; }

        public string OwnerOfRecipeId { get; set; }
        [JsonIgnore]

        public virtual ICollection<FavoriteRecipeUser> FavoriteRecipes { get; set; }
        [JsonIgnore]

        public virtual CookingClasses CookingClass { get; set; }

        //public FoodAppUser FavUserRecipe { get; set; }
        //public Guid FavUserRecipeId { get; set; }



    }
}
