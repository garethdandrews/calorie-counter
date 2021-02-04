using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;

namespace backend_api.Domain.Services
{
    public interface IDiaryEntryService
    {
        Task<IEnumerable<DiaryEntry>> ListAsync();
    }
}