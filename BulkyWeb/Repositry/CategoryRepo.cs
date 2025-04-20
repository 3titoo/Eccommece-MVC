using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BulkyWeb.Repositry
{
    public class CategoryRepo : Irepo<Category>
    {
        ApplicationDbContext _db;
        public CategoryRepo(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return _db.Categories.ToList();
        }

        public Category GetById(int? id)
        {
            return _db.Categories.FirstOrDefault(d => d.Id == id);
        }

        public void Remove(int id)
        {
            _db.Categories.Remove(GetById(id));
            _db.SaveChanges();
        }

        public void Update(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
        }
    }
}
