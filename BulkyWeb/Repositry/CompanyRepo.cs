using BulkyWeb.Data;
using BulkyWeb.Models;

namespace BulkyWeb.Repositry
{
    public class CompanyRepo : Irepo<Company>
    {
        ApplicationDbContext _db;
        public CompanyRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Company item)
        {
            _db.Companies.Add(item);
            _db.SaveChanges();
        }

        public List<Company> GetAll()
        {
            return _db.Companies.ToList();
        }

        public Company GetById(int? id)
        {

            return _db.Companies.FirstOrDefault(x => x.Id == id);
        }


        public void Remove(int id)
        {
            if(GetById(id) != null)
            {
                _db.Companies.Remove(GetById(id));
                _db.SaveChanges();
            }
           
        }

        public void Update(Company item)
        {
            var objFromDb = _db.Companies.FirstOrDefault(x => x.Id == item.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = item.Name;
                objFromDb.StreetAddress = item.StreetAddress;
                objFromDb.City = item.City;
                objFromDb.State = item.State;
                objFromDb.PostalCode = item.PostalCode;
                objFromDb.PhoneNumber = item.PhoneNumber;
                _db.SaveChanges();
            }
        }
    }
}
