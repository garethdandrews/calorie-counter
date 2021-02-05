using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Services.Communication;

namespace backend_api.Domain.Services
{
    public interface IDiaryEntryService
    {
        Task<IEnumerable<DiaryEntry>> ListAsync();
        Task<DiaryEntryResponse> GetDiaryEntryAsync(int userId, string stringDate);
        Task<DiaryEntryResponse> GetDiaryEntryAsync(int userId, DateTime date);
        Task<DiaryEntryResponse> GetUsersDiaryEntryForDateAsync(int userId, DateTime date);
        Task<DiaryEntryResponse> UpdateCalorieTargetAsync(int userId, int calorieTarget);
        Task<DiaryEntryResponse> AddDiaryEntryAsync(int userId, DateTime date);
        Task<DiaryEntryResponse> UpdateDiaryEntryAsync(int id, int calorieTarget);
        Task<DiaryEntryResponse> DeleteDiaryEntryAsync(int id);
    }
}