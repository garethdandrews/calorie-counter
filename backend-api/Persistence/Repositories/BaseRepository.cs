using backend_api.Persistence.Contexts;

namespace backend_api.Persistence.Repositories
{
    /// <summary>
    /// Base repository class
    /// </summary>
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext _context;

        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}