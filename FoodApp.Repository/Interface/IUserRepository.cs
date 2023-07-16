using FoodApp.Models;
using FoodApp.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<FoodAppUser> GetAll();
        FoodAppUser Get(string id);
        void Insert(FoodAppUser entity);
        void Update(FoodAppUser entity);
        void Delete(FoodAppUser entity);
    }
}
