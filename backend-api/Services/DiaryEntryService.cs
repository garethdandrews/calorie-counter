using System.Collections.Generic;
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

        public Task<GetDiaryEntryResponse> GetDiaryEntry(string date)
        {
            
        }

    }
}