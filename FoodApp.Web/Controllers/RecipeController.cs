using FoodApp.Web.Data;
using FoodApp.Web.Data.Dtos;
using FoodApp.Web.Data.Identity;
using FoodApp.Web.Data.Models;
using FoodApp.Web.Data.ViewModel;
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
        private readonly ApplicationDbContext _context;

        public RecipeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Recipe> recipes = _context.Recipes
                .Include(r => r.Ingridients)
                .ToList();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<RecipeDto> recipeDtos = new List<RecipeDto>();
            foreach (var r in recipes)
            {
                List<string> favRecipeOnUsers = _context.FavoriteRecipeUsers.Where(f => f.RecipeId==r.Id)
                    .Select(f => f.UserId).ToList();
                recipeDtos.Add(new RecipeDto
                {
                    Id = r.Id,
                    Title = r.Title,
                    PreparationDescription = r.PreparationDescription,
                    Ingridients = r.Ingridients,
                    OwnerOfRecipe = r.OwnerOfRecipe,
                    OwnerOfRecipeId = r.OwnerOfRecipeId,
                    FavoriteRecipes = r.FavoriteRecipes,
                    IsFavorite = favRecipeOnUsers.Contains(userId) ? true : false,
                });

            }
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
            if (ModelState.IsValid)
            {
                // Create a new Recipe instance and save it
                var recipe = new Recipe
                {
                    Title = viewModel.RecipeTitle,
                    PreparationDescription = viewModel.PreparationDescription,
                    Ingridients = new List<Ingredient>(),
                    FavoriteRecipes = new List<FavoriteRecipeUser>()
                    
                };

                // Convert the IngredientViewModels to Ingredients and add them to the Recipe
                if (viewModel.Ingredients != null)
                {
                    foreach (var ingredientViewModel in viewModel.Ingredients)
                    {
                        var ingredient = new Ingredient
                        {
                            Name = ingredientViewModel.Name,
                            Quantity = ingredientViewModel.Quantity,
                            UnitOfMeasurement = ingredientViewModel.SelectedUnit
                        };
                        recipe.Ingridients.Add(ingredient);
                        _context.Ingridients.Add(ingredient);
                    }
                }
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
                user.AddedRecipes.Add(recipe);
                recipe.OwnerOfRecipeId = userId;

                _context.Users.Update(user);
                _context.Recipes.Add(recipe);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home"); // Redirect to a success page or other appropriate action
            }

            // If the model state is not valid, return the view with the populated ViewModel to display validation errors
            return View(viewModel);
        }

        // Edit: Recipe/Edit
        public async Task<IActionResult> Edit(Guid id)
        {
            Recipe recipe = _context.Recipes.Where(r => r.Id == id)
                .Include(r => r.Ingridients)
                              .Include(r => r.FavoriteRecipes)

                .SingleOrDefaultAsync().Result;

            List<IngredientViewModel> ingredients = new List<IngredientViewModel>();


            foreach(var ingredient in recipe.Ingridients)
            {
                ingredients.Add(new IngredientViewModel
                {
                    IngredientId = ingredient.Id,
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity,
                    SelectedUnit = ingredient.UnitOfMeasurement
                });
            }

            var viewModel = new RecipeViewModel
            {
                RecipeId = id,
                RecipeTitle = recipe.Title,
                PreparationDescription = recipe.PreparationDescription,
                Ingredients = ingredients

            };
            return View(viewModel);
        }

        // POST: Recipe/Edit
        [HttpPost]
        public IActionResult Edit(RecipeViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Recipe recipe = _context.Recipes.Where(r => r.Id == viewModel.RecipeId)
                .Include(r => r.Ingridients)
                .SingleOrDefaultAsync().Result;


                recipe.Title = viewModel.RecipeTitle;
                recipe.PreparationDescription = viewModel.PreparationDescription;
                

                if (viewModel.Ingredients != null)
                {
                    foreach (var ingredientViewModel in viewModel.Ingredients)
                    {
                        Ingredient ingredient = _context.Ingridients.Where(i => i.Id == ingredientViewModel.IngredientId)
                                                .SingleOrDefaultAsync().Result;


                        ingredient.Name = ingredientViewModel.Name;
                        ingredient.Quantity = ingredientViewModel.Quantity;
                        ingredient.UnitOfMeasurement = ingredientViewModel.SelectedUnit;
                        
                        _context.Ingridients.Update(ingredient);
                        _context.SaveChanges();
                    }
                }

                _context.Recipes.Update(recipe);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home"); // Redirect to a success page or other appropriate action
            }

            // If the model state is not valid, return the view with the populated ViewModel to display validation errors
            return View(viewModel);
        }

   
        public IActionResult Delete(Guid id)
        {
            Recipe recipe = _context.Recipes.Where(r => r.Id == id)
                .Include(r => r.Ingridients).SingleOrDefault();  
            if (recipe != null)
            {
                _context.Remove(recipe);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Recipe recipe = _context.Recipes.Where(r => r.Id == id)
                .Include(r => r.Ingridients).SingleOrDefault();
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }



        public IActionResult AddRecipeToFavorites(Guid recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(u => u.Id == userId)
               .FirstOrDefault();

            // Create new order
            Recipe recipe = _context.Recipes.Where(r => r.Id == recipeId)
              .Include(r => r.Ingridients)
              .SingleOrDefaultAsync().Result;

            FavoriteRecipeUser item = new FavoriteRecipeUser
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                User = user,
                RecipeId = recipeId,
                Recipe = recipe
            };

            _context.FavoriteRecipeUsers.Add(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult RemoveRecipeToFavorites(Guid? recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.Where(u => u.Id == userId)
               .FirstOrDefault();

            Recipe recipe = _context.Recipes.Where(r => r.Id == recipeId)
              .Include(r => r.Ingridients)
              .SingleOrDefaultAsync().Result;

            FavoriteRecipeUser favoriteRecipeUser = _context.FavoriteRecipeUsers.Where(f => f.UserId == userId && f.RecipeId == recipeId).SingleOrDefault();

            _context.Remove(favoriteRecipeUser);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}
