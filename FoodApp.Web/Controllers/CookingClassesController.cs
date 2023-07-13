using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodApp.Web.Data;
using FoodApp.Web.Data.Models;

namespace FoodApp.Web.Controllers
{
    public class CookingClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CookingClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CookingClasses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CookingClasses.Include(c => c.Recipe);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CookingClasses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cookingClasses = await _context.CookingClasses
                .Include(c => c.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cookingClasses == null)
            {
                return NotFound();
            }

            return View(cookingClasses);
        }

        // GET: CookingClasses/Create
        public IActionResult Create()
        {
            List<Recipe> recipes = _context.Recipes
          .Include(r => r.CookingClass)
          .Where(r => r.CookingClass == null).ToList();

            ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title");
            return View();
        }

        // POST: CookingClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Link,DateTime,RecipeId")] CookingClasses cookingClasses)
        {
            if (ModelState.IsValid)
            {
                cookingClasses.Id = Guid.NewGuid();
                _context.Add(cookingClasses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            List<Recipe> recipes = _context.Recipes
                .Include(r=>r.CookingClass)
                .Where(r => r.CookingClass == null).ToList();

            ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title", cookingClasses.RecipeId);
            return View(cookingClasses);
        }

        // GET: CookingClasses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cookingClasses = await _context.CookingClasses.FindAsync(id);
            if (cookingClasses == null)
            {
                return NotFound();
            }

            List<Recipe> recipes = _context.Recipes
          .Include(r => r.CookingClass)
          .Where(r => r.CookingClass == null).ToList();

            ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title", cookingClasses.RecipeId);
            return View(cookingClasses);
        }

        // POST: CookingClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Link,DateTime,RecipeId")] CookingClasses cookingClasses)
        {
            if (id != cookingClasses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cookingClasses);
                    await _context.SaveChangesAsync();
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
            List<Recipe> recipes = _context.Recipes
          .Include(r => r.CookingClass)
          .Where(r => r.CookingClass == null).ToList();
            ViewData["RecipeId"] = new SelectList(recipes, "Id", "Title", cookingClasses.RecipeId);
            return View(cookingClasses);
        }

        // GET: CookingClasses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cookingClasses = await _context.CookingClasses
                .Include(c => c.Recipe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cookingClasses == null)
            {
                return NotFound();
            }

            return View(cookingClasses);
        }

        // POST: CookingClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var cookingClasses = await _context.CookingClasses.FindAsync(id);
            _context.CookingClasses.Remove(cookingClasses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CookingClassesExists(Guid id)
        {
            return _context.CookingClasses.Any(e => e.Id == id);
        }
    }
}
