using FoodApp.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models.Models
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public FoodAppUser User { get; set; }
        public virtual ICollection<CookingClassInOrder> ClassesInOrder { get; set; }
    }
}
