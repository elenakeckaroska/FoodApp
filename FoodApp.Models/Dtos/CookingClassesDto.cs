using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models.Dtos
{
    public class CookingClassesDto
    {
        public Guid Id { get; set; }

        public string Link { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Recipe Recipe { get; set; }

        public Guid RecipeId { get; set; }

        public int Price { get; set; }

        public virtual ICollection<CookingClassesInShoppingCart> CookingClassesInShoppingCart { get; set; }

        public virtual ICollection<CookingClassesUser> CookingClassesUser { get; set; }

        public bool isUserSubscribed { get; set; }

        public bool hasPaid { get; set; }

        public int MaxParticipants { get; set; }

        public bool canSubscribeToClass { get; set; }



    }
}
