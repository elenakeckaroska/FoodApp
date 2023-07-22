using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoodApp.Models.Models
{
    public class Ingredient : BaseEntity
    {

        public string Name { get; set; }
        public double Quantity { get; set; }

        public string UnitOfMeasurement { get; set; }

        [JsonIgnore]
        public virtual Recipe Recipe { get; set; }

        public Guid RecipeId { get; set; }
    }
}
