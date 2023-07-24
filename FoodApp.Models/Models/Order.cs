using FoodApp.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FoodApp.Models.Models
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public FoodAppUser User { get; set; }

        [JsonIgnore]
        public virtual ICollection<CookingClassInOrder> ClassesInOrder { get; set; }
    }
}
