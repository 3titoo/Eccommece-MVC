using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Repositry
{
    public class ShoppingCartRepo : IShoppingCartRepositry
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(ShoppingCart item)
        {
            _db.ShoppingCarts.Add(item);
            _db.SaveChanges();
        }

        public List<ShoppingCart> GetAll()
        {
            return _db.ShoppingCarts.Include(u => u.Product).ToList();

        }

        public ShoppingCart GetById(int? id)
        {
            return _db.ShoppingCarts.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<ShoppingCart> GetBYuserID(string userID)
        {
            var q =
                (from cart in _db.ShoppingCarts
                 where cart.ApplicationUserId == userID
                 select cart).Include(u => u.Product).ToList();

            return q;

        }

        public ShoppingCart GetBYuserIDAndProductID(string userID,int? ProductID)
        {
            return _db.ShoppingCarts.FirstOrDefault(u => u.ApplicationUserId == userID && u.ProductId == ProductID);
        }

        public void Remove(int id)
        {
            _db.Remove(_db.ShoppingCarts.FirstOrDefault(u => u.Id == id));
            _db.SaveChanges();
        }

        public void Update(ShoppingCart item)
        {
            _db.ShoppingCarts.Update(item);
            _db.SaveChanges();
        }
    }
}
