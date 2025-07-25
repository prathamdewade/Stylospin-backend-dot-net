using Microsoft.EntityFrameworkCore;
using Stylo_Spin.Models;
using Stylo_Spin.Repository.Defination;

namespace Stylo_Spin.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StyloSpinContext db;

        public CategoryRepository(StyloSpinContext db)
        {
            this.db = db;
        }
        private async Task<bool> Save()
        {
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddCategoryAsync(TblCategory category)
        {
            await db.TblCategories.AddAsync(category);
            return await Save();
        }

        public async Task<bool> DeleteCategoryAsync(TblCategory category)
        {
            db.Remove(category);
            return await Save();
        }

        public async Task<List<TblCategory>> GetAllCategoriesAsync() =>
             await db.TblCategories.Where(c => c.Status).ToListAsync();


        public async Task<TblCategory> GetCategoryByIdAsync(int id)
         => await db.TblCategories.FindAsync(id);
        public async Task<TblCategory> GetCategoryByNameAsync(string name)
        {
            return await db.TblCategories.FirstOrDefaultAsync(c => c.CName == name);
        }



        public async Task<bool> UpdateCategoryAsync(TblCategory category)
        {
            if (category.CId > 0)
                db.TblCategories.Update(category);
            return await Save();
        }
    }
}
