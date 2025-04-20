using BulkyWeb.Data;
using BulkyWeb.Models;

namespace BulkyWeb.Repositry
{
    public class UserRepo : IUserRepositry
    {
        private readonly ApplicationDbContext _db;
        public UserRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(AppUser item)
        {
            throw new NotImplementedException();
        }

        public List<AppUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public AppUser GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public AppUser GetById(string id)
        {
            return _db.AppUseres.FirstOrDefault(u => u.Id == id);
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(AppUser item)
        {
            throw new NotImplementedException();
        }
    }
}
