using FoodApp.Models;
using FoodApp.Models.Identity;
using FoodApp.Repository.Interface;
using FoodApp.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodApp.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<FoodAppUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<FoodAppUser>();
        }
        public IEnumerable<FoodAppUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public FoodAppUser Get(string id)
        {
           return entities
                .Where(u => u.Id == id)
                //.Include(z => z.UserCart)
                .Include(z => z.FavoriteRecipes)
                .Include(z => z.AddedRecipes)
                .First();
        }
        public void Insert(FoodAppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(FoodAppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update
                (entity);
            context.SaveChanges();
        }

        public void Delete(FoodAppUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

    }
}
