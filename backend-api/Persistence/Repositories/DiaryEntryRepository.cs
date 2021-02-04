using System.Collections.Generic;
using System.Linq;
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

        public async Task<DiaryEntry> GetDiaryAsync(int id)
        {
            return await _context.DiaryEntries.Include(x => x.FoodItems).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddDiaryEntryAsync(DiaryEntry diaryEntry)
        {
            await _context.DiaryEntries.AddAsync(diaryEntry);
        }

        public void Update(DiaryEntry diaryEntry)
        {
            _context.DiaryEntries.Update(diaryEntry);
        }

        public async Task<List<DiaryEntry>> GetUsersDiaryEntries(int userId)
        {
            return await _context.DiaryEntries.Where(x => x.User.Id == userId).ToListAsync();
        }
    }
}