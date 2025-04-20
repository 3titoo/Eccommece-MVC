using BulkyWeb.Data;
using BulkyWeb.Models;
using BulkyWeb.Repositry;
using BulkyWeb.Sevice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        Irepo<Category> _db;
        public CategoryController(Irepo<Category> db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var categories = _db.GetAll();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _db.Add(category);
            TempData["success"] = "Category Created Successfully";
            return RedirectToAction("Index");
        }

     

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.GetById(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(int id,Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _db.Update(category);
            TempData["success"] = "Category Edited Successfully";
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var category = _db.GetById(id);
            return View(category);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var category = _db.GetById(id);
            if(category == null)
            {
                return NotFound();
            }
            _db.Remove(id);
            TempData["success"] = "Category Removed Successfully";
            return RedirectToAction("Index");
        }

    }
}
