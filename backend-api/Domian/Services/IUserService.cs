using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Services.Communication;

namespace backend_api.Domain.Services
{
    /// <summary>
    /// The user service.
    /// Handles retreiving, creating and updating users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// Unsuccessful UserResponse if user not found;
        /// Successful UserResponse with the user
        /// </returns>
        Task<UserResponse> GetUserAync(int id);

        /// <summary>
        /// Get user by name (users names are unique)
        /// </summary>
        /// <param name="name"></param>
        /// <returns>
        /// Unsuccessful UserResponse if user not found;
        /// Successful UserResponse with the user
        /// </returns>
        Task<UserResponse> GetUserByNameAsync(string name);

        /// <summary>
        /// Add a new user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userRoles"></param>
        /// <returns>
        /// Unsuccessful UserResponse if the username is already in use;
        /// Unsuccessful UserReponse if there was an issue saving the user to the db;
        /// Successful UserResponse with the user
        /// </returns>
        Task<UserResponse> AddUserAsync(User user, params EApplicationRole[] userRoles);

        /// <summary>
        /// Updates a users calorie target and updates the diary entry for today with the new target
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns>
        /// Unsuccessful UserResponse if user could not be found;
        /// Unsuccessful UserResponse if there was an issue updating the user in the db;
        /// Successful UserResponse with the user
        /// </returns>
        Task<UserResponse> UpdateUserAsync(int id, User user);
    }
}