using FoodApp.Models.Models;
using FoodApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodApp.Repository.Implementation
{
    public class CookingClassesRepository : ICookingClassesRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<CookingClasses> entities;
        string errorMessage = string.Empty;

        public CookingClassesRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<CookingClasses>();
        }

        public void Add(CookingClasses cookingClass)
        {
            this.context.Add(cookingClass);
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            
            context.Remove(this.GetById(id));
            context.SaveChanges();
        }

        public List<CookingClasses> GetAll()
        {
            return this.entities.Include(c => c.Recipe).ToList();
        }

        public CookingClasses GetById(Guid id)
        {
            return entities.Where(c => c.Id == id).Include(c => c.Recipe).FirstOrDefault();
        }

        public void Update(CookingClasses cookingClass)
        {
            this.context.Update(cookingClass);
            context.SaveChanges();
        }
    }
}
