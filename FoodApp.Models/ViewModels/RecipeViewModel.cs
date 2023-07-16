using System;
using System.Collections.Generic;

namespace FoodApp.Models
{
    public class RecipeViewModel
    {
        public Guid RecipeId { get; set; }
        public string RecipeTitle { get; set; }
        public string PreparationDescription { get; set; }

        public List<string> Categories { get; set; }

        public string SelectedCategory { get; set; }
        public List<IngredientViewModel> Ingredients { get; set; }

        public RecipeViewModel()
        {
            Ingredients = new List<IngredientViewModel>();
            Categories = new List<string> { "sweet", "salty", "fitness food"};
        }
    }
}
