using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var categoryFromDb = _db.Categories.Find(id); // if not found Find returns null

            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //similar to find FirstOrDefault return the first entity matching the criteria or returns an exception

            //var categoryFromDbSingle = _db.Categories.SingleOrDefault( c => c.Id == id); ;
            // return the only entity matching the criteria or returns default. Throw execption if more than one entity matches the criteria

            if (categoryFromDb == null) return NotFound();
            return View(categoryFromDb);
        }

        //POST UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var categoryFromDb = _db.Categories.Find(id); 
            if (categoryFromDb == null) return NotFound();
            return View(categoryFromDb);
        }

        //POST DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            // if we were to pass the id instead of the category object we are not to forget the <input asp-for="Id" hidden/> in the form on the delete view  
            if (category == null) return NotFound();
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
