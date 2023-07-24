using FoodApp.Models.Dtos;
using FoodApp.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FoodApp.Service.Interface
{
    public interface IOrderService
    {
        public List<Order> getAllOrders();
        public Order getOrderDetails(Guid id);

        public List<Order> getOrdersForUser(string userId);
        public List<OrderAdminDto> getAllOrdersForAdmin();

        public MemoryStream CreateInvoice(Guid id);
    }
}
