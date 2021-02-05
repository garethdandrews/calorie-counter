using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;

namespace backend_api.Domain.Repositories
{
    public interface IDiaryEntryRepository
    {
        Task<IEnumerable<DiaryEntry>> ListAsync();
        Task<DiaryEntry> GetAsync(int id);
        Task AddAsync(DiaryEntry diaryEntry);
        void Update(DiaryEntry diaryEntry);
        void Remove(DiaryEntry diaryEntry);
        Task<List<DiaryEntry>> GetUsersDiaryEntries(int userId);
        Task<DiaryEntry> GetUsersDiaryEntryForDate(int userId, DateTime date);
    }
}