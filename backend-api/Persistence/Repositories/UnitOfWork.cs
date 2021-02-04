using System.Threading.Tasks;
using backend_api.Domain.Repositories;
using backend_api.Persistence.Contexts;

namespace backend_api.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}