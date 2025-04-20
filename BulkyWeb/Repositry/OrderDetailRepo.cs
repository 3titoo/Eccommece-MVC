using BulkyWeb.Data;
using BulkyWeb.Models;

namespace BulkyWeb.Repositry
{
    public class OrderDetailRepo : IOrderDetailRepositry
    {
        ApplicationDbContext _db;
        public OrderDetailRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(OrderDetail item)
        {
            _db.OrderDetails.Add(item);
            _db.SaveChanges();
        }

        public List<OrderDetail> GetAll()
        {
            return _db.OrderDetails.ToList();
        }

        public OrderDetail GetById(int? id)
        {
            return _db.OrderDetails.FirstOrDefault(d => d.Id == id);
        }

        public void Remove(int id)
        {
            _db.OrderDetails.Remove(GetById(id));
            _db.SaveChanges();
        }

        public void Update(OrderDetail item)
        {
            _db.OrderDetails.Update(item);
            _db.SaveChanges();
        }

    }
}
