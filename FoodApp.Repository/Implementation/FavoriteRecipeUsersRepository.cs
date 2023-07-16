using FoodApp.Models;
using FoodApp.Models.Models;
using FoodApp.Repository.Interface;
using FoodApp.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodApp.Repository.Implementation
{
    public class FavoriteRecipeUsersRepository : IFavoriteRecipeUsersRepository
    {
            private readonly ApplicationDbContext context;
            private DbSet<FavoriteRecipeUser> entities;
            string errorMessage = string.Empty;

            public FavoriteRecipeUsersRepository(ApplicationDbContext context)
            {
                this.context = context;
                entities = context.Set<FavoriteRecipeUser>();
            }

        public void Add(FavoriteRecipeUser item)
        {
            entities.Add(item);
            context.SaveChanges();
        }

        public FavoriteRecipeUser GetByIdAndUser(Guid receiptId, string userId)
        {
            return entities.Where(f => f.UserId == userId && f.RecipeId == receiptId)
                .Include(f => f.Recipe)
                .FirstOrDefault();
        }
        public void Delete(FavoriteRecipeUser model)
        {
            context.Remove(model);
            context.SaveChanges();
        }

        public List<FavoriteRecipeUser> GetFavoriteRecipeUsers()
        {
            return entities.ToList();
        }
    }
}
