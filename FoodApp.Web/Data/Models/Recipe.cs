﻿using FoodApp.Web.Data.Identity;
using System;
using System.Collections.Generic;

namespace FoodApp.Web.Data.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string PreparationDescription { get; set; }

        public virtual ICollection<Ingredient> Ingridients { get; set; }

        public virtual FoodAppUser OwnerOfRecipe { get; set; }

        public string OwnerOfRecipeId { get; set; }

        public virtual ICollection<FavoriteRecipeUser> FavoriteRecipes { get; set; }
        //public FoodAppUser FavUserRecipe { get; set; }
        //public Guid FavUserRecipeId { get; set; }



    }
}