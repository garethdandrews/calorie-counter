using System.Threading.Tasks;
using AutoMapper;
using backend_api.Controllers.Resources.UserResources;
using backend_api.Domain.Models;
using backend_api.Domain.Services;
using backend_api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
     /// <summary>
     /// The user controller.
     /// Handles all incoming requests to get, add and update users.
     /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller 
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="mapper"></param>
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

         /// <summary>
         /// Get a user by ID
         /// </summary>
         /// <param name="id"></param>
         /// <returns>
         /// bad request response if there was an issue retreiving the user;
         /// success response with user
         /// </returns>
        [HttpGet("{username}")]
        public async Task<IActionResult> GetAsync(string username)
        {
            var result = await _userService.GetUserByNameAsync(username);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

         /// <summary>
         /// Add a user
         /// </summary>
         /// <param name="resource">username (string) and password (string)</param>
         /// <returns>
         /// bad request if parameters are not provided;
         /// bad request if there was an issue saving the user to the db;
         /// success response with the user
         /// </returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AddUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<AddUserResource, User>(resource);
            // users that are added through this api route are assigned to the 'common' role
            var result = await _userService.AddUserAsync(user, EApplicationRole.Common);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.User);
            return Ok(userResource);
        }

         /// <summary>
         /// Update a user
         /// </summary>
         /// <param name="id"></param>
         /// <param name="resource">calorie target (int)</param>
         /// <returns>
         /// bad request if parameter is not present;
         /// bad request if there was an issue updating the user in the db;
         /// success response with the user
         /// </returns>
        [HttpPut("{id}")]
        [Authorize]
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