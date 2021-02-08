using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Persistence.Repositories
{

    /// <summary>
    /// The user repository.
    /// Handles CRUD operations
    /// </summary>
    public class UserRepository : BaseRepository, IUserRepository
    {
        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary></summary>
        /// <returns>
        /// A list of users
        /// </returns>
        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users
                    .ToListAsync();
        }

        /// <summary></summary>
        /// <returns>
        /// A user for the given ID, returns null if no user found
        /// </returns>
        public async Task<User> GetAsync(int id)
        {
            return await _context.Users
                    .Include(x => x.Diary)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary></summary>
        /// <returns>
        /// A user for the given name, returns null if no user found
        /// </returns>
        public async Task<User> GetUserByNameAsync(string username)
        {
            return await _context.Users
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .SingleOrDefaultAsync(x => x.Username == username);
        }

        /// <summary>
        /// Adds a user and its roles to the database
        /// </summary>
        /// <returns></returns>
        public async Task AddAsync(User user, EApplicationRole[] userRoles)
        {
            var roleNames = userRoles.Select(x => x.ToString()).ToList();
            var roles = await _context.Roles
                    .Where(x => roleNames.Contains(x.Name))
                    .ToListAsync();

            foreach(var role in roles)
            {
                user.UserRoles.Add(new UserRole {RoleId = role.Id});
            }

            await _context.Users
                    .AddAsync(user);
        }

        /// <summary>
        /// Updates a user in the database
        /// </summary>
        /// <returns></returns>
        public void Update(User user)
        {
            _context.Users
                    .Update(user);
        }
    }
}