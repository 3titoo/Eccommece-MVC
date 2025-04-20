using BulkyWeb.Models;

namespace BulkyWeb.Repositry
{
    public interface IUserRepositry : Irepo<AppUser>
    {
        public AppUser GetById(string id);
    }
}
