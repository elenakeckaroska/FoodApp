using FoodApp.Models.Models;
using FoodApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodApp.Repository.Implementation
{
    public class CookingClassesUserRepository : ICookingClassesUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<CookingClassesUser> entities;
        string errorMessage = string.Empty;

        public CookingClassesUserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<CookingClassesUser>();
        }   

        public List<CookingClassesUser> GetAll()
        {
            return entities.ToList();
        }
        public void Add(CookingClassesUser item)
        {
            entities.Add(item);
            context.SaveChanges();
        }

        public CookingClassesUser GetByIdAndUser(Guid cookingClassId, string userId)
        {
            return entities.Where(f => f.UserId == userId && f.CookingClassesID == cookingClassId)
                .Include(f => f.CookingClass)
                .FirstOrDefault();
        }
        public void Delete(CookingClassesUser model)
        {
            context.Remove(model);
            context.SaveChanges();
        }

        public List<CookingClassesUser> GetFavoriteRecipeUsers()
        {
            return entities.ToList();
        }
    }
}
