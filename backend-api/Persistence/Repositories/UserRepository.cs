using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.Users
                    .ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users
                    .Include(x => x.Diary)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            return await _context.Users
                    .Include(x => x.UserRoles)
                    .ThenInclude(x => x.Role)
                    .SingleOrDefaultAsync(x => x.Name == name);
        }

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

        public void Update(User user)
        {
            _context.Users
                    .Update(user);
        }
    }
}