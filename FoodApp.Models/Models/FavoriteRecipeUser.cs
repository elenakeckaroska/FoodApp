﻿
using FoodApp.Models.Identity;
using System;

namespace FoodApp.Models.Models
{
    public class FavoriteRecipeUser
    {
        public Guid Id { get; set; }
        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public FoodAppUser User { get; set; }

        public string UserId { get; set; }
    }
}
