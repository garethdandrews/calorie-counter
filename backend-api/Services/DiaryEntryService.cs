using System.Collections.Generic;
using System.Threading.Tasks;
using backend_api.Domain.Models;
using backend_api.Domain.Services;

namespace backend_api.Services
{
    public class DiaryEntryService : IDiaryEntryService
    {
        public Task<IEnumerable<DiaryEntry>> ListAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}