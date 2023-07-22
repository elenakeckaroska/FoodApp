using FoodApp.Models.Dtos;
using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Service.Interface
{
    public interface ICookingClassesService
    {
        CookingClasses GetById(Guid Id);

        CookingClasses GetByRecipeId(Guid Id);

        void Create(CookingClasses CookingClass);

        void UserScheduleCookingClass(Guid cookingClassId, string userId);
        void RemoveUserCookingClass(Guid cookingClassId, string userId);

        bool AddToShoppingCart(AddToShoppingCartDto item, string userID);

        List<CookingClassesDto> filterCookingClasses(string userId);

        List<CookingClassesFromAdmin> GetAllForAdmin();
    }
}
