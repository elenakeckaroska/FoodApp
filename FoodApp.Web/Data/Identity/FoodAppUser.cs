﻿using FoodApp.Web.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FoodApp.Web.Data.Identity
{
    public class FoodAppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Recipe> AddedRecipes { get; set; }

        public virtual ICollection<FavoriteRecipeUser> FavoriteRecipes { get; set; }

        //public virtual ShoppingCart UserCart { get; set; }
        //public virtual ICollection<Order> Orders { get; set; }
    }
}
