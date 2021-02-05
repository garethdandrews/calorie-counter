using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Services.Communication;

namespace backend_api.Domain.Services
{
    public interface IUserService
    {
        Task<UserResponse> GetUserAync(int id);
        Task<UserResponse> AddUserAsync(User user, params EApplicationRole[] userRoles);
        Task<UserResponse> UpdateUserAsync(int id, User user);
    }
}