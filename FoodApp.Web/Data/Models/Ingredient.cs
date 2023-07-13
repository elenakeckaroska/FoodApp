using System;
using System.Collections.Generic;

namespace FoodApp.Web.Data.Models
{
    public class Ingredient
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public double Quantity { get; set; }

        public string UnitOfMeasurement { get; set; }

        public virtual Recipe Recipe { get; set; }

        public Guid RecipeId { get; set; }
    }
}
