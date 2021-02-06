using System;
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
    /// The diary entry repository.
    /// Handles CRUD operations
    /// </summary>
    public class DiaryEntryRepository : BaseRepository, IDiaryEntryRepository
    {
        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public DiaryEntryRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary></summary>
        /// <returns>
        /// A list of diary entries
        /// </returns>
        public async Task<IEnumerable<DiaryEntry>> ListAsync()
        {
            return await _context.DiaryEntries
                    .ToListAsync();
        }

        /// <summary></summary>
        /// <param name="id"></param>
        /// <returns>
        /// A diary entry for the given ID, null if no diary entry is found
        /// </returns>
        public async Task<DiaryEntry> GetAsync(int id)
        {
            return await _context.DiaryEntries
                    .Include(x => x.FoodItems)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Add a diary entry to the database
        /// </summary>
        /// <param name="diaryEntry"></param>
        /// <returns></returns>
        public async Task AddAsync(DiaryEntry diaryEntry)
        {
            await _context.DiaryEntries
                    .AddAsync(diaryEntry);
        }

        /// <summary>
        /// Updates a diary entry in the database
        /// </summary>
        /// <param name="diaryEntry"></param>
        public void Update(DiaryEntry diaryEntry)
        {
            _context.DiaryEntries
                    .Update(diaryEntry);
        }

        /// <summary>
        /// Removes a diary entry from the database
        /// </summary>
        /// <param name="diaryEntry"></param>
        public void Remove(DiaryEntry diaryEntry)
        {
            _context.DiaryEntries.Remove(diaryEntry);
        }

        /// <summary>
        /// Get the users diary entry for a given date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="date"></param>
        /// <returns>
        /// The users diary entry for a given date
        /// </returns>
        public async Task<DiaryEntry> GetUsersDiaryEntryForDate(int userId, DateTime date)
        {
            return await _context.DiaryEntries
                    .Include(x => x.FoodItems)
                    .FirstOrDefaultAsync(x => x.User.Id == userId && x.Date == date);
        }
    }
}