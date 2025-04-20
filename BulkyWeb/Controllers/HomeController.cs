using System.Diagnostics;
using System.Security.Claims;
using BulkyWeb.Models;
using BulkyWeb.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Irepo<Product> _pr;
        private readonly IShoppingCartRepositry _sc;


        public HomeController(ILogger<HomeController> logger,Irepo<Product> pr, IShoppingCartRepositry sc)
        {
            _logger = logger;
            _pr = pr;
            _sc = sc;
        }

        public IActionResult Index()
        {
            var data = _pr.GetAll();
            return View(data);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            ShoppingCart cart = new ShoppingCart
            {
                Product = _pr.GetById(id),
                Count = 1,
                ProductId = id
            };
            
            return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart item)
        {
            item.Id = null;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            item.ApplicationUserId = userId;

            if(item.Count <= 0)
            {
                ModelState.AddModelError("Count", "Please enter a valid count.");
                ShoppingCart cart = new ShoppingCart
                {
                    Product = _pr.GetById(item.ProductId),
                    Count = 1,
                    ProductId = item.ProductId
                };

                return View(cart);
            }
            var CartFromDB = _sc.GetBYuserIDAndProductID(userId, item.ProductId);
            if (CartFromDB != null)
            {
                // Item already exists in the cart, update the count
                CartFromDB.Count += item.Count;
                _sc.Update(CartFromDB);
                return RedirectToAction("Index");
            }
            else
            {
                // Item does not exist in the cart, add it
                item.ApplicationUserId = userId;
                _sc.Add(item);
                return RedirectToAction("Index");
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
