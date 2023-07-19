using FoodApp.Models;
using FoodApp.Models.Models;
using FoodApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodApp.Repository.Implementation
{
    public class IngredientRepository : IIngredientRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<Ingredient> entities;
        string errorMessage = string.Empty;

        public IngredientRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Ingredient>();
        }

        public void Add(Ingredient ingredient)
        {
            context.Add(ingredient);
            context.SaveChanges();
        }

        public List<Ingredient> GetAll()
        {
            return entities.ToList();

        }

        public void Update(Ingredient ingredient)
        {
            context.Update(ingredient);
            context.SaveChanges();
        }

        public Ingredient GetById(Guid Id)
        {
            return entities.Where(i => i.Id==Id).FirstOrDefault();
        }
    }
}
