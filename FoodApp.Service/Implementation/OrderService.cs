using FoodApp.Models.Dtos;
using FoodApp.Models.Models;
using FoodApp.Repository.Interface;
using FoodApp.Service.Interface;
using GemBox.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FoodApp.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

     

        public Order getOrderDetails(Guid id)
        {
            return this._orderRepository.getOrderDetails(id);
        }

        public MemoryStream CreateInvoice(Guid id)
        {
            var result = this._orderRepository.getOrderDetails(id);


            string templatePath = $"{Directory.GetCurrentDirectory()}\\files\\invoice.docx";

            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{date}}", DateTime.Now.ToString());
            document.Content.Replace("{{orderId}}", result.Id.ToString());
            document.Content.Replace("{{namesurname}}", result.User.FirstName + " " + result.User.LastName);
            document.Content.Replace("{{username}}", result.User.Email);


            StringBuilder sb = new StringBuilder();

            var total = 0.0;

            foreach (var item in result.ClassesInOrder)
            {
                total += item.SelectedClass.Price;
                sb.AppendLine(item.SelectedClass.Recipe.Title + "                          " +  item.SelectedClass.DateTime +
                    "                          " + item.SelectedClass.Price);
            }

            document.Content.Replace("{{Classes}}", sb.ToString());
            document.Content.Replace("{{total}}", "$" + total.ToString());

            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());

            return stream;
        }

        public List<Order> getOrdersForUser(string userId)
        {
            return _orderRepository.getOrdersForUser(userId);
        }

        public List<OrderAdminDto> getAllOrdersForAdmin()
        {
            return this._orderRepository.getAllOrders()
                .Select(o => new OrderAdminDto()
                {
                    id = o.Id,
                    userId = o.UserId,
                    username = o.User.Email

                }).ToList();
        }
    }
}
