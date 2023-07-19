using FoodApp.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models.Models
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual FoodAppUser Owner { get; set; }

        public virtual ICollection<CookingClassesInShoppingCart> CookingClassesInShoppingCart { get; set; }

        public ShoppingCart() {
            this.CookingClassesInShoppingCart = new List<CookingClassesInShoppingCart>();
                }
    }
}
