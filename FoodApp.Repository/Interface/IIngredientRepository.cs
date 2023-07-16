using FoodApp.Models;
using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Repository.Interface
{
    public interface IIngredientRepository
    {
        List<Ingredient> GetAll();
        void Add(Ingredient ingredient);
        void Update(Ingredient ingredient);

        Ingredient GetById(Guid Id);

    }
}
