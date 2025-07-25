using Stylo_Spin.Models;

namespace Stylo_Spin.Repository.Defination
{
    public interface IContactUsRepository
    {
        Task<bool> CreateAsync(TblContactU contact);
        Task<TblContactU?> GetByIdAsync(int id);
        Task<List<TblContactU>> GetAllAsync();
        Task<bool> DeleteAsync(TblContactU contact);
    }
}
