using FlowerShop.DataAccess.Infrastructure;
using FlowerShop.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerShop.DataAccess.Repositories.RepositoriesImpl
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        // Lấy danh mục theo ID
        public async Task<Category?> GetByIdAsync(int id)
        {
            // Lấy danh mục với điều kiện không bị xóa
            return await DbSet.FirstOrDefaultAsync(c => c.Id == id && !c.IsDelete);
        }

        // Lấy tất cả danh mục chưa bị xóa
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            // Trả về danh sách danh mục chưa bị xóa
            return await DbSet.Where(c => !c.IsDelete).ToListAsync();
        }
    }
}
