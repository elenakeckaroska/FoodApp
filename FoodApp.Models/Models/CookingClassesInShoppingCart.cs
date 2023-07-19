using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models.Models
{
    public class CookingClassesInShoppingCart : BaseEntity
    {
        //public Guid Id { get; set; }
        public Guid CookingClassId { get; set; }
        public virtual CookingClasses CookingClasses { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
