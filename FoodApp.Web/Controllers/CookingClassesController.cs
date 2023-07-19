using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodApp.Models;
using FoodApp.Models.Models;
using FoodApp.Service.Interface;
using FoodApp.Repository.Interface;
using System.Security.Claims;
using FoodApp.Models.Dtos;
using Microsoft.VisualBasic;
using FoodApp.Repository.Implementation;

namespace FoodApp.Web.Controllers
{
    public class CookingClassesController : Controller
    {
        private readonly ICookingClassesService cookingClassesService;
        private readonly ICookingClassesRepository cookingClassesRepository;
        private readonly ICookingClassesUserRepository cookingClassesUserRepository;
        private readonly IRecipeServive recipeService;

        public CookingClassesController(ICookingClassesService cookingClassesService, IRecipeServive recipeService,
            ICookingClassesRepository cookingClassesRepository, ICookingClassesUserRepository cookingClassesUserRepository)
        {
            this.cookingClassesService = cookingClassesService;
            this.cookingClassesRepository = cookingClassesRepository;
            this.recipeService = recipeService;
            this.cookingClassesUserRepository = cookingClassesUserRepository;
        }

        // GET: CookingClasses
        public IActionResult Index()
        {
            List<CookingClasses> cookingClasses = this.cookingClassesRepository.GetAll();
            List<CookingClassesDto> cookingClassesDtos = new List<CookingClassesDto>();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

            foreach (var item in cookingClasses)
            {
                List<string> cook = cookingClassesUserRepository.GetFavoriteRecipeUsers()
                  .Where(f => f.CookingClassesID == item.Id)
                  .Select(f => f.UserId).ToList();
                cookingClassesDtos.Add(
                    new CookingClassesDto
                    {
                        Id = item.Id,
                        Link = item.Link,
                        DateTime = item.DateTime,
                        Recipe = item.Recipe,
                        RecipeId = item.RecipeId,
                        CookingClassesInShoppingCart = item.CookingClassesInShoppingCart,
                        CookingClassesUser = item.CookingClassesUser,
                        isUserSubscribed = cook.Contains(userId) ? true : false,
                        Price = item.Price,
                    });
            }
            return View(cookingClassesDtos);
        }

        // GET: CookingClasses/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cookingClasses = cookingClassesService.GetById(id);
            if (cookingClasses == null)
            {
                return NotFound();
            }

            return View(cookingClasses);
        }

        // GET: CookingClasses/Create
        public IActionResult Create()
        {
            List<Recipe> recipes = recipeService.getAllRecipesWithNullCookingClass();

            ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title");
            return View();
        }

        // POST: CookingClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Link,Price,DateTime,RecipeId")] CookingClasses cookingClasses)
        {
            cookingClassesService.Create(cookingClasses);

            //List<Recipe> recipes = _context.Recipes
            //    .Include(r=>r.CookingClass)
            //    .Where(r => r.CookingClass == null).ToList();

            //ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title", cookingClasses.RecipeId);
            return RedirectToAction("Index");
        }

        // GET: CookingClasses/Edit/5
        public  IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cookingClasses = cookingClassesService.GetById(id);
            if (cookingClasses == null)
            {
                return NotFound();
            }

            List<Recipe> recipes = recipeService.getAllRecipesWithNullCookingClass();

            ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title", cookingClasses.RecipeId);
            return View(cookingClasses);
        }

        // POST: CookingClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Link,Price,DateTime,RecipeId")] CookingClasses cookingClasses)
        {
            if (id != cookingClasses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cookingClassesRepository.Update(cookingClasses);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CookingClassesExists(cookingClasses.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            List<Recipe> recipes = recipeService.getAllRecipesWithNullCookingClass();
            ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title", cookingClasses.RecipeId);
            return View(cookingClasses);
        }

        // GET: CookingClasses/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            cookingClassesRepository.Delete(id);

            CookingClasses cookingClasses = cookingClassesRepository.GetById(id);

            return View(cookingClasses);
        }

        // POST: CookingClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            cookingClassesRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CookingClassesExists(Guid id)
        {
            return cookingClassesRepository.GetById(id) != null;

        }

        public IActionResult UserScheduleCookingClass(Guid cookingClassesID)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            cookingClassesService.UserScheduleCookingClass(cookingClassesID, userId);
            ViewData["subscribed"] = true;
            return RedirectToAction("Index");
        }

        public IActionResult RemoveUserFromCookingClasses(Guid cookingClassesID)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            cookingClassesService.RemoveUserCookingClass(cookingClassesID, userId);

            ViewData["subscribed"] = false;

            return RedirectToAction("Index");

        }

        //public IActionResult AddCookingClassToCart(Guid id)
        //{
        //    var model = this.cookingClassesService.GetById(id);

        //    return View(model);

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCookingClassToCart(Guid classId)
        {

            AddToShoppingCartDto model = new AddToShoppingCartDto { 
                SelectedClassId = classId,
                SelectedClass = cookingClassesService.GetById(classId)
            };

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this.cookingClassesService.AddToShoppingCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            return View("Index");


        }
    }
}
