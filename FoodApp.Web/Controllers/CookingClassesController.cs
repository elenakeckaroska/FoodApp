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


namespace FoodApp.Web.Controllers
{
    public class CookingClassesController : Controller
    {
        private readonly ICookingClassesService cookingClassesService;
        private readonly ICookingClassesRepository cookingClassesRepository;
        private readonly ICookingClassesUserRepository cookingClassesUserRepository;
        private readonly IRecipeServive recipeService;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IOrderService orderService;
        private readonly IRepository<CookingClassInOrder> cookingClassesInOrderRepository;

        public CookingClassesController(ICookingClassesService cookingClassesService, IRecipeServive recipeService,
            ICookingClassesRepository cookingClassesRepository, ICookingClassesUserRepository cookingClassesUserRepository,
            IShoppingCartService shoppingCartService, IOrderService orderService, IRepository<CookingClassInOrder> cookingClassesInOrderRepository)
        {
            this.cookingClassesService = cookingClassesService;
            this.cookingClassesRepository = cookingClassesRepository;
            this.recipeService = recipeService;
            this.cookingClassesUserRepository = cookingClassesUserRepository;
            this.shoppingCartService = shoppingCartService;
            this.orderService = orderService;
            this.cookingClassesInOrderRepository = cookingClassesInOrderRepository;
        }

        // GET: CookingClasses
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<CookingClassesDto> cookingClassesDtos = cookingClassesService.filterCookingClasses(userId);

            return View(cookingClassesDtos);
        }

        public List<CookingClassesFromAdmin> GetAllForAdmin()
        {
            return cookingClassesService.GetAllForAdmin();
        }
        // GET: CookingClasses/Details/5
        //[HttpGet("{id}")]

        public CookingClassesFromAdmin Details(Guid id)
        {
            var cookingClasses = cookingClassesService.GetByRecipeId(id);
            return new CookingClassesFromAdmin()
            {
                Id = cookingClasses.Id,
                Link = cookingClasses.Link,
                DateTime = cookingClasses.DateTime,
                Price = cookingClasses.Price,
                RecipeId  =     cookingClasses.RecipeId,
                MaxParticipants = cookingClasses.MaxParticipants,
                recipeTitle = cookingClasses.Recipe.Title
            };
        }

        public List<CookingClassesUser> GetAllCookingClassesUser()
        {
            return cookingClassesUserRepository.GetAll();
        }

        public List<CookingClassInOrderDto> GetAllCookingClassesInOrderAdmin()
        {
            return cookingClassesInOrderRepository.GetAll()
                .Select(x => new CookingClassInOrderDto()
                {
                    classId = x.ClassId,
                    orderId = x.OrderId
                }).ToList();
        }
        // GET: CookingClasses/Create


        // POST: CookingClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create([FromBody] CookingClassesFromAdmin cookingClasses)
        {
            CookingClasses model = new CookingClasses(){
                Id = Guid.NewGuid(),
                Link = cookingClasses.Link,
                Price = cookingClasses.Price,
                DateTime = cookingClasses.DateTime,
                RecipeId = cookingClasses.RecipeId,
                MaxParticipants = cookingClasses.MaxParticipants
            };
            cookingClassesService.Create(model);

            //List<Recipe> recipes = _context.Recipes
            //    .Include(r=>r.CookingClass)
            //    .Where(r => r.CookingClass == null).ToList();

            //ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title", cookingClasses.RecipeId);
            return RedirectToAction("Index");
        }

        //// GET: CookingClasses/Edit/5
        //public  IActionResult Edit(Guid id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cookingClasses = cookingClassesService.GetById(id);
        //    if (cookingClasses == null)
        //    {
        //        return NotFound();
        //    }

        //    List<Recipe> recipes = recipeService.getAllRecipesWithNullCookingClass();

        //    ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title", cookingClasses.RecipeId);
        //    return View(cookingClasses);
        //}

        // POST: CookingClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public bool Edit([FromBody] CookingClasses cookingClasses)
        {
            if (cookingClasses.Id != cookingClasses.Id)
            {
                return false;
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
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
                return true;
            }
            List<Recipe> recipes = recipeService.getAllRecipesWithNullCookingClass();
            ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title", cookingClasses.RecipeId);
            return true;
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
        public bool DeleteConfirmed(Guid id)
        {
            cookingClassesRepository.Delete(id);
            return true;
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
            //return RedirectToAction("Index");
            AddToShoppingCartDto model = new AddToShoppingCartDto
            {
                SelectedClassId = cookingClassesID,
                SelectedClass = cookingClassesService.GetById(cookingClassesID)
            };

            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this.cookingClassesService.AddToShoppingCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveUserFromCookingClasses(Guid cookingClassesID)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            cookingClassesService.RemoveUserCookingClass(cookingClassesID, userId);

            ViewData["subscribed"] = false;

            shoppingCartService.deleteProductFromSoppingCart(userId, cookingClassesID);

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
