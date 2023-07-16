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
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Recipe> entities;
        string errorMessage = string.Empty;

        public RecipeRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Recipe>();
        }

        public void Add(Recipe recipe)
        {
            context.Add(recipe);
            context.SaveChanges();

        }

        public void Delete(Guid id)
        {
            context.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<Recipe> GetAll()
        {
            return entities
                .Include(r => r.Ingridients)
                .Include(r => r.CookingClass)
                .ToList();
        }
       
        public List<Recipe> GetAllWithNullCookingClass()
        {
            return entities
                .Include(r => r.Ingridients)
                .Include(r => r.CookingClass)
                .Where(r => r.CookingClass==null)
                .ToList();
        }
        public Recipe GetById(Guid id)
        {
            return entities.Where(z => z.Id == id)
                 .Include(r => r.Ingridients)
                .Include(r => r.FavoriteRecipes)
                .FirstOrDefault();
        }

        public void Update(Recipe recipe)
        {
            context.Update(recipe);
            context.SaveChanges();
        }
    }
}
