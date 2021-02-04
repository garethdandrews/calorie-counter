using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;

namespace backend_api.Domain.Repositories
{
    public interface IDiaryEntryRepository
    {
        Task<IEnumerable<DiaryEntry>> ListAsync();
        Task<DiaryEntry> GetDiaryAsync(int id);
        Task AddDiaryEntryAsync(DiaryEntry diaryEntry);
        void Update(DiaryEntry diaryEntry);
        Task<List<DiaryEntry>> GetUsersDiaryEntries(int userId);
    }
}