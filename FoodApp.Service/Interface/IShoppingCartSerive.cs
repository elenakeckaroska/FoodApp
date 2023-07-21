using FoodApp.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteProductFromSoppingCart(string userId, Guid classId);
        bool order(string userId);
    }
}
