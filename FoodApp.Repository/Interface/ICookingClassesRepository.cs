using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Repository.Interface
{
    public interface ICookingClassesRepository
    {
        List<CookingClasses> GetAll();
        CookingClasses GetById(Guid id);

        void Add(CookingClasses cookingClass);

        void Update(CookingClasses cookingClass);
        void Delete(Guid id);

    


    }
}
