using System;
using System.Collections.Generic;

namespace FoodApp.Models.Models
{
    public class CookingClasses : BaseEntity
    {
        //public Guid Id { get; set; }

        public string Link { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Recipe Recipe { get; set; }

        public int Price { get; set; }
        public Guid RecipeId { get; set; }

        public virtual ICollection<CookingClassesInShoppingCart> CookingClassesInShoppingCart { get; set; }

        public virtual ICollection<CookingClassesUser> CookingClassesUser { get; set; }

        
    }
}
