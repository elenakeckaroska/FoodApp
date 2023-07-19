using FoodApp.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models.Models
{
    public class CookingClassesUser
    {
        public Guid Id { get; set; }
        public Guid CookingClassesID { get; set; }
        public CookingClasses CookingClass { get; set; }

        public FoodAppUser User { get; set; }

        public string UserId { get; set; }

    }
}
