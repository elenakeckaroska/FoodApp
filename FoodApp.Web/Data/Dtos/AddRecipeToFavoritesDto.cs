using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using FoodApp.Web.Data.Models;

namespace FoodApp.Web.Data.Dtos
{
    public class AddRecipeToFavoritesDto
    {
        public Recipe SelectedRecipe { get; set; }
        public Guid SelectedRecipeId { get; set; }

   
    }
}
