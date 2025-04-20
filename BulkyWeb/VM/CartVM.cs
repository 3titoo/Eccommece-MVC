using BulkyWeb.Models;

namespace BulkyWeb.VM
{
    public class CartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public OrderHeader orderHeader { get; set; }

    }
}
