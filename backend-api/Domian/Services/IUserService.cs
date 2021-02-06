using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Services.Communication;

namespace backend_api.Domain.Services
{
    public interface IUserService
    {
        /**
         * Returns the user for the given ID
         */
        Task<UserResponse> GetUserAync(int id);

        /**
         * Returns the user for the given name (users have unique names)
         */
        Task<User> GetUserByNameAsync(string name);

        /**
         * Adds a user to the database
         */
        Task<UserResponse> AddUserAsync(User user, params EApplicationRole[] userRoles);

        /**
         * Updates an existing user in the database
         */
        Task<UserResponse> UpdateUserAsync(int id, User user);
    }
}