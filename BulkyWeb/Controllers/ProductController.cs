using BulkyWeb.Models;
using BulkyWeb.Repositry;
using BulkyWeb.Sevice;
using BulkyWeb.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        Irepo<Product> _db;
        Irepo<Category> cat;
        IWebHostEnvironment _hostingEnvironment;

        public ProductController(Irepo<Product> db, Irepo<Category> cat, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            this.cat = cat;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var products = _db.GetAll();
           
            return View(products);
        }
        public IActionResult Upsert(int? id)
        {
            var pr = new productVM()
            {
                pr = new Product(),
                categoryList = cat.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return View(pr);
            }
            else
            {
                pr.pr = _db.GetById(id.GetValueOrDefault());
                return View(pr);
            }
        }

        [HttpPost]
        public IActionResult Upsert(productVM product,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(webRootPath, @"images\products");

                    if(!string.IsNullOrEmpty(product.pr.ImgUrl))
                    {
                        var imagePath = Path.Combine(webRootPath, product.pr.ImgUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    product.pr.ImgUrl = @"\images\products\" + fileName;

                }
                if (product.pr.Id != 0)
                {
                    _db.Update(product.pr);
                    TempData["success"] = "Product Updated Successfully";
                }
                else
                {
                    _db.Add(product.pr);
                    TempData["success"] = "Product Added Successfully";
                }
                return RedirectToAction("Index");
            }
            else
            {
                product.categoryList = cat.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(product);
            }
        }


        public IActionResult Delete(int id)
        {
            var product = _db.GetById(id);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            var product = _db.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            _db.Remove(id);
            TempData["success"] = "Product Removed Successfully";
            return RedirectToAction("Index");
        }
    }
}
