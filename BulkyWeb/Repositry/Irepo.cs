using BulkyWeb.Models;

namespace BulkyWeb.Repositry
{
    public interface Irepo<T> where T : class
    {
        List<T> GetAll();
        T GetById(int? id);
        void Add(T item);
        void Remove(int id);
        void Update(T item);

    }
}
