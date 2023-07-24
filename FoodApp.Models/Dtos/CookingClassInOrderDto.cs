using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace FoodApp.Models.Dtos
{
    public class CookingClassInOrderDto
    {
        public Guid classId { get; set; }
        [JsonIgnore]
        public CookingClasses selectedClass { get; set; }

        public Guid orderId { get; set; }
    }
}
