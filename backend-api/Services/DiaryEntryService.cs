using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;
using backend_api.Resources.DiaryEntryResources;
using backend_api.Services.Helpers;

namespace backend_api.Services
{
    public class DiaryEntryService : IDiaryEntryService
    {
        private IDiaryEntryRepository _diaryEntryRepository;

        public DiaryEntryService(IDiaryEntryRepository diaryEntryRepository)
        {
            _diaryEntryRepository = diaryEntryRepository;
        }

        public async Task<IEnumerable<DiaryEntry>> ListAsync()
        {
            return await _diaryEntryRepository.ListAsync();
        }

        public async Task<DiaryEntryResponse> GetDiaryEntry(int userId, string stringDate)
        {
            DateTime date;
            try
            {
                date = StringDateHelper.ConverStringDateToDate(stringDate)
            }
            catch (ArgumentException e)
            {
                return new DiaryEntryResponse(e.Message);
            }

            return await GetDiaryEntry(userId, date);
        }

        public async Task<DiaryEntryResponse> GetDiaryEntry(int userId, DateTime date)
        {
            var usersDiaryEntries = await _diaryEntryRepository.GetUsersDiaryEntries(userId);

            if (usersDiaryEntries.Count == 0)
                return new DiaryEntryResponse("User has no diary entries.");

            var diaryEntry = usersDiaryEntries.FirstOrDefault(x => x.Date == date);

            if (diaryEntry == null)
                return new DiaryEntryResponse("No diary found for that day.");

            var diaryEntryWithFoodItems = await _diaryEntryRepository.GetDiaryAsync(diaryEntry.Id);

            return new DiaryEntryResponse(diaryEntryWithFoodItems);
        }

    }
}