using System.Threading.Tasks;
using backend_api.Domain.Repositories;
using backend_api.Persistence.Contexts;

namespace backend_api.Persistence.Repositories
{
    /// <summary>
    /// Unit of work service
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Saves the changes made to the db
        /// </summary>
        /// <returns></returns>
        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}