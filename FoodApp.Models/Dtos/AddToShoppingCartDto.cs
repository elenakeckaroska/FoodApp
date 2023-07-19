using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodApp.Models.Dtos
{
    public class AddToShoppingCartDto
    {
        public CookingClasses SelectedClass { get; set; }
        public Guid SelectedClassId { get; set; }

        //[Display(Name = "Number of tickets")]
        //public int SelectedQuantity { get; set; }
    }
}
