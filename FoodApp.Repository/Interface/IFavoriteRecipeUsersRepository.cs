using FoodApp.Models;
using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Repository.Interface
{
    public interface IFavoriteRecipeUsersRepository
    {
        List<FavoriteRecipeUser> GetFavoriteRecipeUsers();

        void Add(FavoriteRecipeUser item);
        void Delete(FavoriteRecipeUser model);

        FavoriteRecipeUser GetByIdAndUser(Guid receiptId, string userId);
    }
}
