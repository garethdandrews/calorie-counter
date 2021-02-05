using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using backend_api.Domain.Models;
using backend_api.Domain.Services;
using backend_api.Extensions;
using backend_api.Resources.DiaryEntryResources;
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
        
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromBody] GetDiaryEntryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _diaryEntryService.GetDiaryEntry(resource.UserId, resource.StringDate);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var diaryEntryResource = _mapper.Map<DiaryEntry, DiaryEntryResource>(result.DiaryEntry);
            return Ok(diaryEntryResource);
        }
    }
}