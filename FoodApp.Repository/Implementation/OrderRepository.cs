using FoodApp.Models.Models;
using FoodApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodApp.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> getAllOrders()
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.ClassesInOrder)
                .Include("ClassesInOrder.SelectedClass")
                .ToListAsync().Result;
        }

        public Order getOrderDetails(Guid id)
        {
            return entities
               .Include(z => z.User)
               .Include(z => z.ClassesInOrder)
               .Include("ClassesInOrder.SelectedClass")
               .Include("ClassesInOrder.SelectedClass.Recipe")
               .SingleOrDefaultAsync(z => z.Id == id).Result;
        }

        public List<Order> getOrdersForUser(string userId)
        {
            return entities
                .Where(z => z.UserId == userId)
                .Include(z => z.ClassesInOrder)
                .Include("ClassesInOrder.SelectedClass")
                .Include("ClassesInOrder.SelectedClass.Recipe")
                .ToListAsync().Result;
        }

    }
}
