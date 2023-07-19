using FoodApp.Models.Identity;
using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models.Dtos
{
    public class ShoppingCartDto : BaseEntity
    {
        public double TotalPrice { get; set; }


        public virtual ICollection<CookingClassesInShoppingCart> CookingClassesInShoppingCart { get; set; }

    }
}
