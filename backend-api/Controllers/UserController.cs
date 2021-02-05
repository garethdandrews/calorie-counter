using System.Threading.Tasks;
using AutoMapper;
using backend_api.Domain.Models;
using backend_api.Domain.Services;
using backend_api.Extensions;
using backend_api.Resources.UserResources;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    [Route("api/[controller")]
    public class UserController : Controller 
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _userService.GetUserAync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<AddUserResource, User>(resource);
            var result = await _userService.AddUserAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<UpdateUserResource, User>(resource);
            var result = await _userService.UpdateUserAsync(id, user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

    }
}