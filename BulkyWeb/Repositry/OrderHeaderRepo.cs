using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BulkyWeb.Repositry
{
    public class OrderHeaderRepo : IOrderHeaderRepositry
    {
        ApplicationDbContext _db;
        public OrderHeaderRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(OrderHeader item)
        {
            _db.OrderHeaders.Add(item);
            _db.SaveChanges();
        }

        public List<OrderHeader> GetAll()
        {
            return _db.OrderHeaders.ToList();
        }

        public OrderHeader GetById(int? id)
        {
            return _db.OrderHeaders.FirstOrDefault(d => d.Id == id);
        }

        public void Remove(int id)
        {
            _db.OrderHeaders.Remove(GetById(id));
            _db.SaveChanges();
        }

        public void Update(OrderHeader item)
        {
            _db.OrderHeaders.Update(item);
            _db.SaveChanges();
        }
    }
}
