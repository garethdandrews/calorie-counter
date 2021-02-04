using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using backend_api.Domain.Models;
using backend_api.Domain.Services;
using backend_api.Extensions;
using backend_api.Resources.DiaryEntry;
using backend_api.Resources.FoodItem;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    [Route("api/[controller")]
    public class DiaryEntryController : Controller 
    {
        private readonly IDiaryEntryService _diaryEntryService;
        private readonly IMapper _mapper;

        public DiaryEntryController(IDiaryEntryService diaryEntryService, IMapper mapper)
        {
            _diaryEntryService = diaryEntryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DiaryEntryResource>> GetAllAsync()
        {
            var diaryEntries = await _diaryEntryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<DiaryEntry>, IEnumerable<DiaryEntryResource>>(diaryEntries);

            return resources;
        }
    }
}