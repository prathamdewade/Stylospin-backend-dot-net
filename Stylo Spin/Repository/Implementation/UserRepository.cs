using Microsoft.EntityFrameworkCore;
using Stylo_Spin.Models;
using Stylo_Spin.Repository.Defination;

namespace Stylo_Spin.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly StyloSpinContext db;

        public UserRepository(StyloSpinContext db)
        {
            this.db = db;
        }
        public async Task<bool> AddUserAsync(TblUser user)
        {
            await db.AddAsync(user);
            return await Save();
        }

        public async Task<bool> DeleteUserAsync(TblUser user)
        {
            db.Remove(user);
            return await Save();
        }

        public async Task<IEnumerable<TblUser>> GetAllUsersAsync()
       => await db.TblUsers.ToListAsync();
        public Task<TblUser> GetUserByEmailAsync(string email)
       => db.TblUsers.FirstOrDefaultAsync(u => u.UserEmail == email);

        public async Task<TblUser> GetUserByIdAsync(int id)
       => await db.TblUsers.FindAsync(id);

        public async Task<TblUser> GetUserByNameAsync(string name) => await db.TblUsers.FirstOrDefaultAsync(u => u.Username == name);


        public async Task<bool> IsEmailExistsAsync(string email) => await db.TblUsers.FirstOrDefaultAsync(u => u.UserEmail == email) != null;

        public async Task<bool> Save() => await db.SaveChangesAsync() > 0;


        public async Task<bool> UpdateUserAsync(TblUser user)
        {
            db.TblUsers.Update(user);
            return await Save();
        }
    }
}
