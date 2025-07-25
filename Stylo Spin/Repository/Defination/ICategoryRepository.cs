using Stylo_Spin.Models;

namespace Stylo_Spin.Repository.Defination
{
    //this interface is used to define the contract for category repository for crud
    public interface ICategoryRepository
    {
        public Task<List<TblCategory>> GetAllCategoriesAsync();
        public Task<TblCategory> GetCategoryByIdAsync(int id);
        public Task<TblCategory> GetCategoryByNameAsync(string name);
        public Task<bool> AddCategoryAsync(TblCategory category);
        public Task<bool> UpdateCategoryAsync(TblCategory category);
        public Task<bool> DeleteCategoryAsync(TblCategory category);
    



    }
}
