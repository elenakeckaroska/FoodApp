using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodApp.Repository.Interface
{
    public interface IOrderRepository
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(Guid id);
        public List<Order> getOrdersForUser(string userId);

    }
}
