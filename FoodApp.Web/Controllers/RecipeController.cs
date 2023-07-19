using FoodApp.Models;
using FoodApp.Models.Dtos;
using FoodApp.Models.Models;
using FoodApp.Models.ViewModels;
using FoodApp.Repository.Interface;
using FoodApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodApp.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeServive recipeService;
        private readonly IRecipeRepository recipeRepository;


        public RecipeController(IRecipeServive recipeServive, IRecipeRepository recipeRepository)
        {
            this.recipeService = recipeServive;
            this.recipeRepository = recipeRepository;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipeDtos = recipeService.getAllRecipesDto(userId);
            return View(recipeDtos);
        }
        // GET: Recipe/Add
        public IActionResult Add()
        {
            var viewModel = new RecipeViewModel();
            return View(viewModel);
        }

        // POST: Recipe/Add
        [HttpPost]
        public IActionResult Add(RecipeViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            bool ifSuccessfully = recipeService.Add(viewModel, userId);
            if(ifSuccessfully)
                return RedirectToAction("Index");
            else
             return View(viewModel);
        }

        //// Edit: Recipe/Edit
        public IActionResult Edit(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            RecipeViewModel viewModel = recipeService.EditGet(id, userId);
            return View(viewModel);
        }

        //// POST: Recipe/Edit
        [HttpPost]
        public IActionResult Edit(RecipeViewModel viewModel)
        {
            recipeService.EditPost(viewModel);
            return RedirectToAction("Index"); // Redirect to a success page or other appropriate action


        }

   
        public IActionResult Delete(Guid id)
        {
            recipeService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(Guid id)
        {

            Recipe recipe = recipeRepository.GetById(id);
           
            return View(recipe);
        }



        public IActionResult AddRecipeToFavorites(Guid recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            recipeService.AddRecipeToFavorites(recipeId, userId);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveRecipeToFavorites(Guid recipeId)
       {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            recipeService.RemoveRecipeFromFavorites(recipeId, userId);

            return RedirectToAction("Index");

       }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddBilletToCart(AddToShoppingCartDto model)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    //var userShoppingCard = await _context.ShoppingCarts.Where(z => z.OwnerId.Equals(userId)).FirstOrDefaultAsync();
        //    var result = this.recipeService.AddToShoppingCart(model, userId);

        //    if (result)
        //    {
        //        return RedirectToAction("Index", "ShoppingCart");
        //    }
        //    return View(model);


        //}

    }
}
