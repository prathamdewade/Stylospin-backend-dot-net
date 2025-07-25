using Stylo_Spin.Models;

namespace Stylo_Spin.Repository.Defination
{
    public interface ISliderRepository
    {
        Task<List<TblSlider>> GetAllAsync();
        Task<TblSlider?> GetByIdAsync(int id);
        Task<bool> AddAsync(TblSlider slider);
        Task<bool> DeleteAsync(TblSlider slider);
        Task<bool> UpdateSliderAsync(TblSlider slider);
        Task<bool> SaveChangesAsync();
    }
}
