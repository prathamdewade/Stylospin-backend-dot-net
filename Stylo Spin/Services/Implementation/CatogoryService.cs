using Stylo_Spin.Dtos;
using Stylo_Spin.Models;
using Stylo_Spin.Repository.Defination;
using Stylo_Spin.Services.Defination;
using Microsoft.Extensions.Logging;

namespace Stylo_Spin.Services.Implementation
{
    public class CategoryService : ICatgoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ICategoryRepository repo, ILogger<CategoryService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<bool> CreateCategoryAsync(CategoryDto dto)
        {
            _logger.LogInformation("Attempting to create category: {CategoryName}", dto.CName);

            var existing = await _repo.GetCategoryByNameAsync(dto.CName);
            if (existing != null)
            {
                _logger.LogWarning("Category already exists: {CategoryName}", dto.CName);
                return false;
            }

            var category = new TblCategory
            {
                CName = dto.CName,
                Status = dto.Status
            };

            var result = await _repo.AddCategoryAsync(category);
            _logger.LogInformation("Category creation {Status} for: {CategoryName}", result ? "succeeded" : "failed", dto.CName);
            return result;
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryDto dto)
        {
            _logger.LogInformation("Attempting to update category ID: {CategoryId}", id);

            var existing = await _repo.GetCategoryByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Category not found for update: ID = {CategoryId}", id);
                return false;
            }

            existing.CName = dto.CName;
            existing.Status = dto.Status;

            var result = await _repo.UpdateCategoryAsync(existing);
            _logger.LogInformation("Category update {Status} for ID: {CategoryId}", result ? "succeeded" : "failed", id);
            return result;
        }

        public async Task<List<TblCategory>> GetAllCategoriesAsync()
        {
            _logger.LogInformation("Retrieving all categories...");
            var result = await _repo.GetAllCategoriesAsync();
            _logger.LogInformation("Retrieved {Count} categories.", result?.Count ?? 0);
            return result;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            _logger.LogInformation("Attempting to delete category with ID: {CategoryId}", id);

            var existing = await _repo.GetCategoryByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Category not found for deletion: ID = {CategoryId}", id);
                return false;
            }

            var result = await _repo.DeleteCategoryAsync(existing);
            _logger.LogInformation("Category deletion {Status} for ID: {CategoryId}", result ? "succeeded" : "failed", id);
            return result;
        }
    }
}
