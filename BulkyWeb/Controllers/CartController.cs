using BulkyWeb.Models;
using BulkyWeb.Repositry;
using BulkyWeb.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWeb.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IShoppingCartRepositry _shoppingCartRepositry;
        private readonly IUserRepositry _userRepositry;
        CartVM cartVM;
        public CartController(IShoppingCartRepositry shoppingCartRepositry,IUserRepositry userRepositry)
        {
            _userRepositry = userRepositry;
            _shoppingCartRepositry = shoppingCartRepositry;
            cartVM = new CartVM()
            {
                ShoppingCarts = new List<ShoppingCart>(),
                orderHeader = new OrderHeader() { OrderTotal = 0}
            };
        }
        public IActionResult Index()
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            cartVM.ShoppingCarts = _shoppingCartRepositry.GetBYuserID(userId);

            foreach (var cart in cartVM.ShoppingCarts)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                cartVM.orderHeader.OrderTotal += cart.Price * cart.Count;
            }

            return View(cartVM);
        }

        public IActionResult plus(int cartId)
        {
            var cart = _shoppingCartRepositry.GetById(cartId);
            cart.Count += 1;
            _shoppingCartRepositry.Update(cart);
            return RedirectToAction("Index");
        }
        public IActionResult minus(int cartId)
        {
            var cart = _shoppingCartRepositry.GetById(cartId);
            if (cart.Count <= 1)
            {
                _shoppingCartRepositry.Remove(cartId);
            }
            else
            {
                cart.Count -= 1;
                _shoppingCartRepositry.Update(cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            cartVM.ShoppingCarts = _shoppingCartRepositry.GetBYuserID(userId);

            cartVM.orderHeader.AppUser = _userRepositry.GetById(userId);

            cartVM.orderHeader.Name = cartVM.orderHeader.AppUser.Name;
            cartVM.orderHeader.StreetAddress = cartVM.orderHeader.AppUser.StreetAddress;
            cartVM.orderHeader.City = cartVM.orderHeader.AppUser.City;
            cartVM.orderHeader.State = cartVM.orderHeader.AppUser.State;
            cartVM.orderHeader.PostalCode = cartVM.orderHeader.AppUser.PostalCode;
            cartVM.orderHeader.PhoneNumber = cartVM.orderHeader.AppUser.PhoneNumber;


            foreach (var cart in cartVM.ShoppingCarts)
            {
                cart.Price = GetPriceBasedOnQuantity(cart);
                cartVM.orderHeader.OrderTotal += cart.Price * cart.Count;
            }

            return View(cartVM);
        }
        public IActionResult delete(int cartId)
        {
            _shoppingCartRepositry.Remove(cartId);
            return RedirectToAction("Index");
        }

        private double GetPriceBasedOnQuantity(ShoppingCart sc)
        {
            if(sc.Count <= 50)
            {
                return sc.Product.Price;
            } else if(sc.Count <= 100)
            {
                return sc.Product.Price50;
            }
            else 
            {
                return sc.Product.Price100;
            }

        }
    }
}
