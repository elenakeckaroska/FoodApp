using FoodApp.Models.Dtos;
using FoodApp.Models.Models;
using FoodApp.Repository.Implementation;
using FoodApp.Repository.Interface;
using FoodApp.Service.Events;
using FoodApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace FoodApp.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<CookingClassInOrder> _cookingClassesInOrder;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<CookingClasses> _coookingClassesRepository;
        private readonly IRepository<EmailMessage> _mailRepository;
        private readonly OrderCompletionNotifier _orderCompletionNotifier;



        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository,
            IUserRepository userRepository, IRepository<CookingClasses> coookingClassesRepository,
            IRepository<Order> orderRepository, IRepository<CookingClassInOrder> cookingClassesInOrder,
            IRepository<EmailMessage> mailRepository, OrderCompletionNotifier orderCompletionNotifier)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _coookingClassesRepository = coookingClassesRepository;
            _orderRepository = orderRepository;
            _cookingClassesInOrder = cookingClassesInOrder;
            //_productInOrderRepository = productInOrderRepository;
            _mailRepository = mailRepository;
            //_productRepository = productRepository;
            _orderCompletionNotifier = orderCompletionNotifier;
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

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCard = loggedInUser.UserCart;

                EmailMessage message = new EmailMessage();
                message.MailTo = loggedInUser.Email;
                message.Subject = "Succesfully created order";
                message.Status = false;


                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };



                this._orderRepository.Insert(order);

                List<CookingClassInOrder> classesInOrder = new List<CookingClassInOrder>();

                var result = userCard.CookingClassesInShoppingCart
                    .Select(z => new CookingClassInOrder
                {
                    Id = Guid.NewGuid(),
                    ClassId = z.CookingClasses.Id,
                    SelectedClass = z.CookingClasses,
                    OrderId = order.Id,
                    UserOrder = order
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("HAPPY BIRTHDAY!! <3");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var currentItem = result[i - 1];
                    totalPrice += currentItem.SelectedClass.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.SelectedClass.Price + " with quantity of: and price of: $" + currentItem.SelectedClass.Price);
                }

                //sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                message.Content = sb.ToString();

                classesInOrder.AddRange(result);

                foreach (var item in classesInOrder)
                {
                    this._cookingClassesInOrder
                        .Insert(item);
                }

                loggedInUser.UserCart.CookingClassesInShoppingCart.Clear();

                this._userRepository.Update(loggedInUser);
                this._mailRepository.Insert(message);

                _orderCompletionNotifier.NotifyOrderCompleted(order);

                return true;
            }

            return false;
        }

    }
}
