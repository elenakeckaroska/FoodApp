using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Models.Dtos
{
    public class OrderAdminDto
    {

        public Guid id { get; set; }
        public string userId { get; set; }
        public string username { get; set; }
        public virtual ICollection<CookingClassInOrder> classesInOrder { get; set; }

        public OrderAdminDto()
        {
            this.classesInOrder = new List<CookingClassInOrder>();

        }
    }
}
