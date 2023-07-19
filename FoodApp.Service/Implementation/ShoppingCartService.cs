using FoodApp.Models.Dtos;
using FoodApp.Models.Models;
using FoodApp.Repository.Interface;
using FoodApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace FoodApp.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        //private readonly IRepository<Order> _orderRepository;
        //private readonly IRepository<EmailMessage> _mailRepository;
        //private readonly IRepository<BilletInOrders> _productInOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<CookingClasses> _coookingClassesRepository;
        //private readonly OrderCompletionNotifier _orderCompletionNotifier;



        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository,
            IUserRepository userRepository, IRepository<CookingClasses> coookingClassesRepository)

        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _coookingClassesRepository = coookingClassesRepository;
            //_productInOrderRepository = productInOrderRepository;
            //_mailRepository = mailRepository;
            //_productRepository = productRepository;
            //_orderCompletionNotifier = orderCompletionNotifier;
        }


        public bool deleteProductFromSoppingCart(string userId, Guid classId)
        {
            if (!string.IsNullOrEmpty(userId) && classId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.CookingClassesInShoppingCart
                    .Where(z => z.CookingClassId.Equals(classId)).FirstOrDefault();

                userShoppingCart.CookingClassesInShoppingCart.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCard = loggedInUser.UserCart; ;

                var allProducts = userCard.CookingClassesInShoppingCart.ToList();


                var allProductPrices = allProducts.Select(z => new
                {
                    ProductPrice = z.CookingClasses.Price,
                }).ToList();

                double totalPrice = 0.0;

                foreach (var item in allProductPrices)
                {
                    totalPrice += item.ProductPrice;
                }

                var result = new ShoppingCartDto
                {
                    CookingClassesInShoppingCart = allProducts,
                    TotalPrice = totalPrice
                };

                return result;
            }
            return new ShoppingCartDto();
        }

    //    public bool order(string userId)
    //    {
    //        if (!string.IsNullOrEmpty(userId))
    //        {
    //            var loggedInUser = this._userRepository.Get(userId);
    //            var userCard = loggedInUser.UserCart;

    //            EmailMessage mail = new EmailMessage();
    //            mail.MailTo = loggedInUser.Email;
    //            mail.Subject = "Sucessfuly created order!";
    //            mail.Status = false;


    //            Order order = new Order
    //            {
    //                Id = Guid.NewGuid(),
    //                User = loggedInUser,
    //                UserId = userId,
    //                OrderDate = DateTime.Now
    //            };

    //            this._orderRepository.Insert(order);

    //            List<BilletInOrders> productInOrders = new List<BilletInOrders>();

    //            var result = userCard.BilletInShoppingCart.Select(z => new BilletInOrders
    //            {
    //                Id = Guid.NewGuid(),
    //                BilletId = z.CurrentBillet.Id,
    //                Billet = z.CurrentBillet,
    //                OrderId = order.Id,
    //                Order = order,
    //                Quantity = z.Quantity
    //            }).ToList();

    //            StringBuilder sb = new StringBuilder();

    //            var totalPrice = 0.0;

    //            sb.AppendLine("Your order is completed. The order conatins: ");

    //            for (int i = 1; i <= result.Count(); i++)
    //            {
    //                var currentItem = result[i - 1];
    //                totalPrice += currentItem.Quantity * currentItem.Billet.BilletPrice;
    //                sb.AppendLine(i.ToString() + ". Ticket/s for " + currentItem.Billet.BilletName + "movie for: " + currentItem.Quantity + "people with price of: $" + currentItem.Billet.BilletPrice);
    //            }

    //            sb.AppendLine("Total price for your order: " + totalPrice.ToString());

    //            mail.Content = sb.ToString();


    //            productInOrders.AddRange(result);

    //            foreach (var item in productInOrders)
    //            {
    //                this._productInOrderRepository.Insert(item);

    //                var billet = this._productRepository.Get(item.Billet.Id);
    //                billet.Quantity = billet.Quantity - item.Quantity;
    //                if (billet.Quantity < 0)
    //                {
    //                    billet.Quantity = 0;
    //                }
    //                this._productRepository.Update(billet);

    //            }

    //            loggedInUser.UserCart.BilletInShoppingCart.Clear();

    //            this._userRepository.Update(loggedInUser);
    //            this._mailRepository.Insert(mail);


    //            //mailNotifier
    //            _orderCompletionNotifier.NotifyOrderCompleted(order);




    //            return true;
    //        }


    //        return false;
    //    }


    }
}
