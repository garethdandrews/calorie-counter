using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Services.Communication;

namespace backend_api.Domain.Services
{
    public interface IDiaryEntryService
    {
        Task<IEnumerable<DiaryEntry>> ListAsync();
        Task<GetDiaryEntryResponse> GetDiaryEntry(int userId, string stringDate);
    }
}