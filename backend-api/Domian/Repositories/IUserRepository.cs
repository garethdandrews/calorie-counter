using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;

namespace backend_api.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task<User> GetUserAsync(int id);
        Task AddDiaryEntryAsync(User user);
        void Update(User user);
    }
}