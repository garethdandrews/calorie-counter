using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;

namespace backend_api.Domain.Repositories
{
    public interface IUserRepository
    {
        /**
         * List the 
         */
        Task<IEnumerable<User>> ListAsync();
        Task<User> GetAsync(int id);
        Task<User> GetUserByNameAsync(string name);
        Task AddAsync(User user, EApplicationRole[] userRoles);
        void Update(User user);
    }
}