using FoodApp.Models;
using FoodApp.Models.Dtos;
using FoodApp.Models.Models;
using FoodApp.Models.ViewModels;
using FoodApp.Repository.Interface;
using FoodApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodApp.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeServive recipeService;
        private readonly IRecipeRepository recipeRepository;
        private readonly IRepository<Ingredient> ingredientRepository;

        public RecipeController(IRecipeServive recipeServive, IRecipeRepository recipeRepository, IRepository<Ingredient> ingredientRepository)
        {
            this.recipeService = recipeServive;
            this.recipeRepository = recipeRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public List<Recipe> GetAll()
        {
            return recipeRepository.GetAll();
        }
        public List<Ingredient> GetAllIngredients()
        {
            return ingredientRepository.GetAll().ToList();
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipeDtos = recipeService.getAllRecipesDto(userId);
            List<string> categories = recipeDtos.Select(r => r.Category).Distinct().ToList();
            ViewData["categories"] = categories;

            return View(recipeDtos);
             
            

            

        }
        [HttpPost]
        public IActionResult Index(string selectedCategory) {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recipeDtos = recipeService.getAllRecipesDtoWithCategory(userId, selectedCategory);

            var recipeDtos1 = recipeService.getAllRecipesDto(userId);

            List<string> categories = recipeDtos1.Select(r => r.Category).Distinct().ToList();

            ViewData["categories"] = categories;

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

        public IActionResult DownloadFileForWritingRecipe()
        {
            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\recipe.xlsx";

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            // Provide the suggested file name for the user to download
            string fileName = "recipe.xlsx";

            // Return the file as a downloadable content result
            return File(System.IO.File.OpenRead(filePath), contentType, fileName);
        }

     
        [HttpPost]
        public async Task<IActionResult> ReadingFromFileUsers(IFormFile file)
        {
            string fileName = file.FileName;
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\filesWriten\\{fileName}";

            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            RecipeViewModel recipe = recipeService.ReadRecipeFromFile(fileName);

            this.Add(recipe);
        

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
