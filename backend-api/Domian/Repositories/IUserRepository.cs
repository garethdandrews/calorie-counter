using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;

namespace backend_api.Domain.Repositories
{
    /// <summary>
    /// The user repository.
    /// Handles CRUD operations
    /// </summary>
    public interface IUserRepository
    {
        /// <summary></summary>
        /// <returns>
        /// Returns a list of users
        /// </returns>
        Task<IEnumerable<User>> ListAsync();
        
        /// <summary></summary>
        /// <returns>
        /// Returns a list of users
        /// </returns>
        Task<User> GetAsync(int id);

        /// <summary></summary>
        /// <returns>
        /// Returns a list of users
        /// </returns>
        Task<User> GetUserByNameAsync(string name);

        /// <summary>
        /// Adds a user and its roles to the database
        /// </summary>
        /// <returns></returns>
        Task AddAsync(User user, EApplicationRole[] userRoles);

        /// <summary>
        /// Updates a user in the database
        /// </summary>
        /// <returns></returns>
        void Update(User user);
    }
}