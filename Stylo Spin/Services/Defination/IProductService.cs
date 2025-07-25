
using Stylo_Spin.Models;
using Stylo_Spin.Dtos;

namespace Stylo_Spin.Services.Defination
{
    public interface IProductService
    {
        public Task<List<TblProduct>> GetAllProductsAsync();
        public Task<TblProduct> GetProductByIdAsync(int id);
        public Task<TblProduct> CreateProductAsync(ProductDto product);
        public Task<TblProduct> UpdateProductAsync(int id, ProductDto product);
        public Task<bool> DeleteProductAsync(int id);
        public Task<List<TblProduct>> GetProductsByCategoryAsync(int categoryId);
        //  public Task<List<TblProduct>> GetProductsByBrandAsync(int brandId);
        public Task<List<TblProduct>> SearchProductsAsync(string searchTerm);
        public Task<List<TblProduct>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        //  public Task<List<TblProduct>> GetProductsByRatingAsync(decimal minRating, decimal maxRating);

    }
}
