using FoodApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Security.Claims;

namespace FoodApp.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }


        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(this._shoppingCartService.getShoppingCartInfo(userId));
        }

        public Boolean OrderNow()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._shoppingCartService.order(userId);

            return result;
        }
        
        public IActionResult PayOrder(String stripeEmail, String stripeToken)
        {
            var customerService = new CustomerService();
            var chargeservice = new ChargeService();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = this._shoppingCartService.getShoppingCartInfo(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeservice.Create(new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(order.TotalPrice * 100),
                Description = "EShop Application Payment",
                Currency = "usd",
                Customer = customer.Id

            });

            if (charge.Status == "succeeded")
            {
                var result = this.OrderNow();

                if (result)
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }
                else
                {
                    return RedirectToAction("Index", "ShoppingCart");
                }
            }

            return RedirectToAction("Index", "ShoppingCart");
        }
    }

}
