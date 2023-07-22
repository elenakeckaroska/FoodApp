using FoodApp.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FoodApp.Models.Models
{
    public class CookingClassesUser
    {
        public Guid Id { get; set; }
        public Guid CookingClassesID { get; set; }

        [JsonIgnore]
        public CookingClasses CookingClass { get; set; }

        [JsonIgnore]
        public FoodAppUser User { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

    }
}
