using FoodApp.Models;
using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Repository.Interface
{
    public interface IRecipeRepository
    {
        List<Recipe> GetAllWithNullCookingClass();
        List<Recipe> GetAll();

        void Add(Recipe recipe);
        Recipe GetById(Guid id);

        void Update(Recipe recipe);

        void Delete(Guid id);
    }
}
