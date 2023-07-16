using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Service.Interface
{
    public interface ICookingClassesService
    {
        CookingClasses GetById(Guid Id);
        void Create(CookingClasses CookingClass);
    }
}
