using Stylo_Spin.Models;

namespace Stylo_Spin.Repository.Defination
{
    public interface IUserRepository
    {
        Task<IEnumerable<TblUser>> GetAllUsersAsync();
        Task<TblUser> GetUserByIdAsync(int id);
        Task<TblUser> GetUserByEmailAsync(string email);
        Task<TblUser> GetUserByNameAsync(string name);
        Task<bool> AddUserAsync(TblUser user);
        Task<bool> UpdateUserAsync(TblUser user);
        Task<bool> DeleteUserAsync(TblUser user);
        Task<bool> Save();
        Task<bool> IsEmailExistsAsync(string email);
    }
}
