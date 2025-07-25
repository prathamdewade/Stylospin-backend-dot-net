using Stylo_Spin.Models;

namespace Stylo_Spin.Repository.Defination
{
    public interface IProductRepository
    {
        Task<List<TblProduct>> GetAllProducts();
        Task<TblProduct?> GetProductById(int id);
        Task<TblProduct?> GetProductByName(string name);
        Task<bool> AddProduct(TblProduct product);
        Task<bool?> UpdateProduct(TblProduct product);
        Task<bool> DeleteProduct(TblProduct product);
        Task<bool> Save();
    }
}
