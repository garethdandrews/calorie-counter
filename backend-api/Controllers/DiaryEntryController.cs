using System.Threading.Tasks;
using AutoMapper;
using backend_api.Controllers.Resources.DiaryEntryResources;
using backend_api.Domain.Models;
using backend_api.Domain.Services;
using backend_api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    /// <summary>
    /// The diary entry controller.
    /// Handles all incoming requests to get a users diary entry
    /// </summary>
    [Route("api/[controller]")]
    public class DiaryEntryController : Controller 
    {
        private readonly IDiaryEntryService _diaryEntryService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="diaryEntryService"></param>
        /// <param name="mapper"></param>
        public DiaryEntryController(IDiaryEntryService diaryEntryService, IMapper mapper)
        {
            _diaryEntryService = diaryEntryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the diary entry for a given user ID and date
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="stringDate">must be in the format dd-mm-yyyy</param>
        /// <returns>
        /// bad request if parameters are invalid;
        /// bad request if there was an issue retreiving the diary entry;
        /// success response with the diary entry
        /// </returns>
        [HttpGet("{userId}/{stringDate}")]
        public async Task<IActionResult> GetAsync(int userId, string stringDate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _diaryEntryService.GetDiaryEntryAsync(userId, stringDate);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var diaryEntryResource = _mapper.Map<DiaryEntry, DiaryEntryResource>(result.DiaryEntry);
            return Ok(diaryEntryResource);
        }
    }
}