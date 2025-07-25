using Microsoft.EntityFrameworkCore;
using Stylo_Spin.Models;
using Stylo_Spin.Repository.Defination;

namespace Stylo_Spin.Repository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly StyloSpinContext db;

        public ProductRepository(StyloSpinContext db)
        {
            this.db = db;
        }

        public async Task<List<TblProduct>> GetAllProducts()
        {
            return await db.TblProducts.Include(p => p.CIdNavigation).ToListAsync();
        }

        public async Task<TblProduct?> GetProductById(int id)
        {
            return await db.TblProducts.Include(p => p.CIdNavigation).FirstOrDefaultAsync(p => p.PId == id);
        }

        public async Task<bool> AddProduct(TblProduct product)
        {
            await db.TblProducts.AddAsync(product);
            return await Save();
        }

        public async Task<bool?> UpdateProduct(TblProduct product)
        {
            db.TblProducts.Update(product);
            return await Save();
        }

        public async Task<bool> DeleteProduct(TblProduct product)
        {
            db.TblProducts.Remove(product);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<TblProduct?> GetProductByName(string name)
        {
            return await db.TblProducts
                .Include(p => p.CIdNavigation)
                .FirstOrDefaultAsync(p => p.PName == name);
        }

    }

}
