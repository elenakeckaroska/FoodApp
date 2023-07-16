using System;
using System.Collections.Generic;

namespace FoodApp.Models
{
    public class IngredientViewModel
    {
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public List<string> UnitOfMeasurement { get; set; } 

        public string SelectedUnit { get; set; }

        public IngredientViewModel()
        {
            UnitOfMeasurement = new List<string> { "cup/s", "kg" };
        }
    }


}
