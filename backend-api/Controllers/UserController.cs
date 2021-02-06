using System.Threading.Tasks;
using AutoMapper;
using backend_api.Controllers.Resources.UserResources;
using backend_api.Domain.Models;
using backend_api.Domain.Services;
using backend_api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
    /**
     *  The user controller. Handles all incoming requests to get, add and update users.
     */
    [Route("api/[controller]")]
    public class UserController : Controller 
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /**
         * Get a user by ID
         * Return: bad request response if there was an issue retreiving the user
         * Return: success response
         */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _userService.GetUserAync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        /**
         * Add a user
         * Parameters: username (string) and password (string)
         * Return: bad request if parameters are not present
         * Return: bad request if there was an issue saving the user to the db
         * Return: success response
         */
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<AddUserResource, User>(resource);
            var result = await _userService.AddUserAsync(user, EApplicationRole.Common);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

        /**
         * Update a user
         * Parameters: calorie target (int)
         * Return: bad request if parameter is not present
         * Return: bad request if there was an issue updating the user in the db
         * Return: success response
         */
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