using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Repositry
{
    public class ProductRepo : Irepo<Product>
    {
        ApplicationDbContext _db;
        public ProductRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return _db.Products.Include(c=>c.Category).ToList();
        }

        public Product GetById(int? id)
        {
            var products = _db.Products.Include(c=>c.Category).FirstOrDefault(x => x.Id == id);
            return products;

        }

        public void Remove(int id)
        {
            _db.Products.Remove(GetById(id));
            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
        }
    }
}
