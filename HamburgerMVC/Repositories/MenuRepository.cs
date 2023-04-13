using HamburgerMVC.Models;
using HamburgerMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HamburgerMVC.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly Context _dbContext;

        public MenuRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _dbContext.Menus.ToListAsync();
        }

        public async Task<Menu> GetByIdAsync(int id)
        {
            return await _dbContext.Menus.FindAsync(id);
        }
    }
}
