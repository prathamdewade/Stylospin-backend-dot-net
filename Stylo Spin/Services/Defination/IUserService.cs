using Microsoft.AspNetCore.Identity;
using Stylo_Spin.Dtos;
using Stylo_Spin.Models;

namespace Stylo_Spin.Services.Defination
{
    public interface IUserService
    {
        Task<IEnumerable<TblUser>> GetAllUsersAsync();    
        Task<bool> AddUserAsync(TblUser user);
        Task<string> Login(UserLogin dto);
        Task<bool> UpdateUserAsync(int id ,TblUser user);
        Task<bool> DeleteUserAsync(int userId);


    }
}
