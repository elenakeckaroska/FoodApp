using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models.Dtos
{
    public class CookingClassesFromAdmin
    {
        public Guid Id { get; set; }
        public string Link { get; set; }
        public DateTime DateTime { get; set; }
        public int Price { get; set; }
        public Guid RecipeId { get; set; }
        public int MaxParticipants { get; set; }

        public string recipeTitle { get; set; }

        
    }
}
