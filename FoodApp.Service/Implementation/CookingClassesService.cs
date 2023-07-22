using FoodApp.Models.Dtos;
using FoodApp.Models.Models;
using FoodApp.Repository.Implementation;
using FoodApp.Repository.Interface;
using FoodApp.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace FoodApp.Service.Implementation
{
    public class CookingClassesService : ICookingClassesService
    {
        public readonly ICookingClassesRepository cookingClassesRepository;
        public readonly IUserRepository userRepository;
        public readonly ICookingClassesUserRepository cookingClassesUserRepository;
        private readonly IRepository<CookingClassesInShoppingCart> cookingClassesInShoppingCartRepository;
        private readonly IRepository<CookingClassInOrder> cookingClassesInOrderRepository;
        private readonly IOrderService orderService;


        public CookingClassesService(ICookingClassesRepository cookingClassesRepository, IUserRepository userRepository,
            ICookingClassesUserRepository cookingClassesUserRepository,
            IRepository<CookingClassesInShoppingCart> _cookingClassesInShoppingCartRepository, IRepository<CookingClassInOrder> cookingClassesInOrderRepository, IOrderService orderService)
        {
            this.cookingClassesRepository = cookingClassesRepository;
            this.userRepository = userRepository;
            this.cookingClassesUserRepository = cookingClassesUserRepository;
            this.cookingClassesInShoppingCartRepository = _cookingClassesInShoppingCartRepository;
            this.cookingClassesInOrderRepository = cookingClassesInOrderRepository;
            this.orderService = orderService;
        }
        public CookingClasses GetById(Guid Id)
        {
            return cookingClassesRepository.GetById(Id);
        }

        public void Create(CookingClasses cookingClasses)
        {
            cookingClassesRepository.Add(cookingClasses);
        }

        public void UserScheduleCookingClass(Guid cookingClassId, string userId)
        {
            var user = userRepository.Get(userId);

            CookingClasses cookingClasses = cookingClassesRepository.GetById(cookingClassId);


            CookingClassesUser item = new CookingClassesUser
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                User = user,
                Username = user.Email,
                CookingClassesID = cookingClassId,
                CookingClass = cookingClasses
            };

            cookingClassesUserRepository.Add(item);
        }

        public void RemoveUserCookingClass(Guid cookingClassId, string userId)
        {
            var user = userRepository.Get(userId);

            CookingClasses cookingClasses = cookingClassesRepository.GetById(cookingClassId);

            CookingClassesUser cookingClassesUser = cookingClassesUserRepository.GetByIdAndUser(cookingClassId, userId);

            cookingClassesUserRepository.Delete(cookingClassesUser);
        }

        public bool AddToShoppingCart(AddToShoppingCartDto item, string userID)
        {
            var user = this.userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.SelectedClassId != null && userShoppingCard != null)
            {
                var cookingClass = this.cookingClassesRepository.GetById(item.SelectedClassId);
                //{896c1325-a1bb-4595-92d8-08da077402fc}

                if (cookingClass != null)
                {
                    CookingClassesInShoppingCart itemToAdd = new CookingClassesInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        CookingClasses = cookingClass,
                        CookingClassId = cookingClass.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                    };

                    //var existing = userShoppingCard.CookingClassesInShoppingCart
                    //    .Where(z => z.ShoppingCartId == userShoppingCard.Id && z.CookingClassId == itemToAdd.CookingClassId)
                    //    .FirstOrDefault();

                    //if (existing != null)
                    //{
                    //    existing.Quantity += itemToAdd.Quantity;
                    //    this._productInShoppingCartRepository.Update(existing);

                    //}
                    //else
                    //{
                    //    this._productInShoppingCartRepository.Insert(itemToAdd);
                    //}

                    this.cookingClassesInShoppingCartRepository.Insert(itemToAdd);


                    return true;
                }
                return false;
            }
            return false;
        }

        public List<CookingClassesDto> filterCookingClasses(string userId)
        {
            List<CookingClasses> cookingClasses = this.cookingClassesRepository.GetAll();
            List<CookingClassesDto> cookingClassesDtos = new List<CookingClassesDto>();


            foreach (var item in cookingClasses)
            {
                List<string> cook = cookingClassesUserRepository.GetFavoriteRecipeUsers()
                  .Where(f => f.CookingClassesID == item.Id)
                  .Select(f => f.UserId).ToList();

                List<Order> ordersByUser = orderService.getAllOrders()
                    .Where(o => o.UserId == userId)
                    .ToList();

                bool paid = false;
                foreach (var order in ordersByUser)
                {
                    if (order.ClassesInOrder.Select(f => f.ClassId)
                        .Contains(item.Id))
                    {
                        paid = true;
                    }
                }

                int currentSubscribedUsers = cookingClassesInOrderRepository.GetAll()
                    .Where(c => c.ClassId == item.Id)
                    .Count();

                bool canSubscribe = false;

                if (currentSubscribedUsers < item.MaxParticipants)
                    canSubscribe = true;

                cookingClassesDtos.Add(
                    new CookingClassesDto
                    {
                        Id = item.Id,
                        Link = item.Link,
                        DateTime = item.DateTime,
                        Recipe = item.Recipe,
                        RecipeId = item.RecipeId,
                        MaxParticipants = item.MaxParticipants,
                        CookingClassesInShoppingCart = item.CookingClassesInShoppingCart,
                        CookingClassesUser = item.CookingClassesUser,
                        isUserSubscribed = cook.Contains(userId) ? true : false,
                        Price = item.Price,
                        hasPaid = paid,
                        canSubscribeToClass = canSubscribe,
                    });
            }

            return cookingClassesDtos;
        }

        public CookingClasses GetByRecipeId(Guid Id)
        {
            return cookingClassesRepository.GetAll()
                .Where(c => c.RecipeId == Id)
                .FirstOrDefault();
        }

        public List<CookingClassesFromAdmin> GetAllForAdmin()
        {
            return cookingClassesRepository.GetAll()
                .Select(c => new CookingClassesFromAdmin()
                {
                    Id = c.Id,
                    RecipeId = c.RecipeId,
                    Link = c.Link,
                    DateTime = c.DateTime,
                    Price = c.Price,
                    MaxParticipants = c.MaxParticipants,
                    recipeTitle = c.Recipe.Title
                }).ToList();

        }
    }
}
