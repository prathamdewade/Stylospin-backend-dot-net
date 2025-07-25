using Microsoft.EntityFrameworkCore;
using Stylo_Spin.Models;
using Stylo_Spin.Repository.Defination;

namespace Stylo_Spin.Repository.Implementation
{
    public class AboutUsRepository : IAboutUsRepository
    {
        private readonly StyloSpinContext db;

        public AboutUsRepository(StyloSpinContext db)
        {
            this.db = db;
        }
        public async Task<bool> Save() => await db.SaveChangesAsync() > 0;
        public async Task<bool> DeleteAysnc(AboutU u)
        {
            db.AboutUs.Remove(u);
            return await Save();
        }

        public async Task<List<AboutU>> GetAboutUsAsync()

         => await db.AboutUs.ToListAsync();


        public async Task<AboutU> GetAboutUSByIdAsync(int id)
       => await db.FindAsync<AboutU>(id);

        public async Task<bool> UpdateAysnc(AboutU u)
        {
            db.AboutUs.Update(u);
            return await Save();
        }

     

        public async Task<bool> CreateAbobutUs(AboutU u)
        {
            await db.AboutUs.AddAsync(u);
            return await Save(); // make sure SaveChangesAsync > 0
        }
    }
}
