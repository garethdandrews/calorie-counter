using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Repositories;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;
using backend_api.Resources.DiaryEntryResources;

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

        public async Task<GetDiaryEntryResponse> GetDiaryEntry(int userId, string stringDate)
        {
            DateTime date;
            if (DateTime.TryParse(stringDate, out date))
                string.Format("{0:d/MM/yyyy}", date);
            else
                return new GetDiaryEntryResponse("Invalid date. Must be in the format dd/MM/yyyy.");

            var usersDiaryEntries = await _diaryEntryRepository.GetUsersDiaryEntries(userId);

            if (usersDiaryEntries.Count == 0)
                return new GetDiaryEntryResponse("User has no diary entries.");

            var diaryEntry = usersDiaryEntries.FirstOrDefault(x => x.Date == date);

            if (diaryEntry == null)
                return new GetDiaryEntryResponse("No diary found for that day.");

            var diaryEntryWithFoodItems = await _diaryEntryRepository.GetDiaryAsync(diaryEntry.Id);

            return new GetDiaryEntryResponse(diaryEntryWithFoodItems);
        }

    }
}