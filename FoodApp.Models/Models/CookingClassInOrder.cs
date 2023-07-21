using FoodApp.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models.Models
{
    public class CookingClassInOrder : BaseEntity
    {
        public Guid ClassId { get; set; }
        public CookingClasses SelectedClass { get; set; }

        public Guid OrderId { get; set; }

        public Order UserOrder { get; set; }

    }
}
