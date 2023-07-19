using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Repository.Interface
{
    public interface ICookingClassesUserRepository
    {
        List<CookingClassesUser> GetFavoriteRecipeUsers();

        void Add(CookingClassesUser item);
            void Delete(CookingClassesUser model);

        CookingClassesUser GetByIdAndUser(Guid cookingClassesId, string userId);
    }
}
