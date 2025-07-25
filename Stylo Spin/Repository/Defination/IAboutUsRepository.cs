using Stylo_Spin.Models;

namespace Stylo_Spin.Repository.Defination
{
    public interface IAboutUsRepository
    {
        public Task<List<AboutU>> GetAboutUsAsync();
        public Task<AboutU> GetAboutUSByIdAsync(int id);
         
        public Task<bool> CreateAbobutUs(AboutU u);
        public Task<bool> DeleteAysnc(AboutU u);
        public Task<bool> UpdateAysnc(AboutU u);
        
    }
}
