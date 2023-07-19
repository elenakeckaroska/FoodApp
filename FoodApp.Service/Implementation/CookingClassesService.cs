using FoodApp.Models.Dtos;
using FoodApp.Models.Models;
using FoodApp.Repository.Implementation;
using FoodApp.Repository.Interface;
using FoodApp.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodApp.Service.Implementation
{
    public class CookingClassesService : ICookingClassesService
    {
        public readonly ICookingClassesRepository cookingClassesRepository;
        public readonly IUserRepository userRepository;
        public readonly ICookingClassesUserRepository cookingClassesUserRepository;
        private readonly IRepository<CookingClassesInShoppingCart> cookingClassesInShoppingCartRepository;


        public CookingClassesService(ICookingClassesRepository cookingClassesRepository, IUserRepository userRepository,
            ICookingClassesUserRepository cookingClassesUserRepository,
            IRepository<CookingClassesInShoppingCart> _cookingClassesInShoppingCartRepository)
        {
            this.cookingClassesRepository = cookingClassesRepository;
            this.userRepository = userRepository;
            this.cookingClassesUserRepository = cookingClassesUserRepository;
            this.cookingClassesInShoppingCartRepository = _cookingClassesInShoppingCartRepository;
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
    }
}
