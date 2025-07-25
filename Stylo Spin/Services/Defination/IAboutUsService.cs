using Stylo_Spin.Dtos;
using Stylo_Spin.Models;

namespace Stylo_Spin.Services.Defination
{
    public interface IAboutUsService
    {
        Task<List<AboutU>> GetAllAsync();
        Task<AboutU> GetByIdAsync(int id);
        Task<AboutU> CreateAsync(AboutuUsDto dto);
        Task<AboutU> UpdateAsync(int id, AboutuUsDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
