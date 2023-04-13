using HamburgerMVC.Models;

namespace HamburgerMVC.Repositories.Interfaces
{
    public interface IMenuRepository
    {
        Task<IEnumerable<Menu>> GetAllAsync();
        Task<Menu> GetByIdAsync(int id);
    }
}
