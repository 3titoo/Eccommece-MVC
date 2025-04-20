using BulkyWeb.Models;

namespace BulkyWeb.Repositry
{
    public interface IShoppingCartRepositry : Irepo<ShoppingCart>
    {
        public ShoppingCart GetBYuserIDAndProductID(string userID, int? ProductID);
        public IEnumerable<ShoppingCart> GetBYuserID(string userID);

    }
}