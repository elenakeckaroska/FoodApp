using FoodApp.Models.Models;
using FoodApp.Service.Interface;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using FoodApp.Models.Dtos;

namespace FoodApp.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;


        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");

        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Order> allOrders = this._orderService.getOrdersForUser(userId);

            return View(allOrders);
        }
        public List<OrderAdminDto> GetOrders()
        {
            return this._orderService.getAllOrdersForAdmin();
        }
        public IActionResult CreateInvoice(Guid id)
        {

            var stream = _orderService.CreateInvoice(id);

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportOrderInvoice.pdf");


        }
    }
}
