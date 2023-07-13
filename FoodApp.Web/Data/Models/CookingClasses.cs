using System;

namespace FoodApp.Web.Data.Models
{
    public class CookingClasses
    {
        public Guid Id { get; set; }

        public string Link { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Recipe Recipe { get; set; }

        public Guid RecipeId { get; set; }

    }
}
