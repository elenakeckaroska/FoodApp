using FoodApp.Models.Models;
using FoodApp.Repository.Interface;
using FoodApp.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Service.Implementation
{
    public class CookingClassesService : ICookingClassesService
    {
        public readonly ICookingClassesRepository cookingClassesRepository;

        public CookingClassesService(ICookingClassesRepository cookingClassesRepository)
        {
            this.cookingClassesRepository = cookingClassesRepository;
        }
        public CookingClasses GetById(Guid Id)
        {
            return cookingClassesRepository.GetById(Id);
        }

        public void Create(CookingClasses cookingClasses)
        {
            cookingClassesRepository.Add(cookingClasses);
        }
    }
}
