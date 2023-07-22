using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoodApp.Models.Models
{
    public class CookingClasses : BaseEntity
    {
        //public Guid Id { get; set; }

        public string Link { get; set; }
        public DateTime DateTime { get; set; }

        [JsonIgnore]
        public virtual Recipe Recipe { get; set; }

        public int Price { get; set; }
        public Guid RecipeId { get; set; }
        public int MaxParticipants { get; set; }

        [JsonIgnore]
        public virtual ICollection<CookingClassesInShoppingCart> CookingClassesInShoppingCart { get; set; }

        [JsonIgnore]
        public virtual ICollection<CookingClassesUser> CookingClassesUser { get; set; }
        [JsonIgnore]
        public virtual ICollection<CookingClassInOrder> Orders { get; set; }

    }
}
