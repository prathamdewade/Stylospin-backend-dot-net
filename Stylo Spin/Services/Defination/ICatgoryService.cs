using Stylo_Spin.Dtos;
using Stylo_Spin.Models;

namespace Stylo_Spin.Services.Defination
{
    public interface ICatgoryService
    {
        Task<List<TblCategory>> GetAllCategoriesAsync();
        Task<bool> CreateCategoryAsync(CategoryDto dto);
        Task<bool> UpdateCategoryAsync(int id, CategoryDto dto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
