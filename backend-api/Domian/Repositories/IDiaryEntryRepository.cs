using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;

namespace backend_api.Domain.Repositories
{
    public interface IDiaryEntryRepository
    {
        Task<IEnumerable<DiaryEntry>> ListAsync();
    }
}