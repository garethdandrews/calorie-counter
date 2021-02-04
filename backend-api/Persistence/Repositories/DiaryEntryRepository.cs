using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace backend_api.Persistence.Repositories
{
    public class DiaryEntryRepository : BaseRepository, IDiaryEntryRepository
    {
        public DiaryEntryRepository(ApplicationDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<DiaryEntry>> ListAsync()
        {
            return await _context.DiaryEntries.ToListAsync();
        }
    }
}