using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using FoodApp.Models.Models;

namespace FoodApp.Models.Dtos
{
    public class AddRecipeToFavoritesDto 
    {
        public Recipe SelectedRecipe { get; set; }
        public Guid SelectedRecipeId { get; set; }

   
    }
}
