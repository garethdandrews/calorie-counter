using System.Threading.Tasks;
using AutoMapper;
using backend_api.Controllers.Resources.TokenResources;
using backend_api.Controllers.Resources.UserResources;
using backend_api.Domain.Security.Tokens;
using backend_api.Domain.Services;
using backend_api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace backend_api.Controllers
{
     /// <summary>
     /// The login controller.
     /// Handles the assigning, refreshing and revoking of access tokens.
     /// Access tokens are tokens that contain claims and are used by the API to validate specific user data.
     /// Access tokens contain:
     ///     - encoded token used to access api endpoints, that expire 30 seconds after creating,
     ///     - refresh token, an encoded token that expires 900 seconds after token is created,
     ///     - expiration date of the access token for validation,
     /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Handles the dependencies
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="authenticationService"></param>
        public LoginController(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

         /// <summary>
         /// Validates the user credentials then issues an access token
         /// </summary>
         /// <param name="resource">username (string) and password (string)</param>
         /// <returns>
         /// bad request if credentials are not provided;
         /// bad request if credentials are invalid;
         /// otherwise, access token
         /// </returns>
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserCredentialsResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _authenticationService.CreateAccessTokenAsync(resource.Name, resource.Password);

            if (!result.Success)
                return BadRequest(result.Message);

            var accessTokenResource = _mapper.Map<AccessToken, AccessTokenResource>(result.AccessToken);
            return Ok(accessTokenResource);
        }

        /// <summary>
        /// Validates the refresh token and the username, then issues a new access token
        /// </summary>
        /// <param name="resource">refresh token (string) username (string)</param>
        /// <returns>
        /// bad request if refresh token or username are not provided;
        /// bad request if token is invalid, expired or username does not exist;
        /// otherwise, access token
        /// </returns>
        [Route("/token/refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _authenticationService.RefreshTokenAsync(resource.Token, resource.Name);

            if (!result.Success)
                return BadRequest(result.Message);

            var tokenResource = _mapper.Map<AccessToken, AccessTokenResource>(result.AccessToken);
            return Ok(tokenResource);
        }

        /// <summary>
        /// Validates the refresh token then removes the access token
        /// Called when a user signs out to prevent security issues
        /// </summary>
        /// <param name="resource">refresh token (string)</param>
        /// <returns>
        /// bad request if refresh token not provided
        /// no content
        /// </returns>
        [Route("/token/revoke")]
        [HttpPost]
        public IActionResult RevokeToken([FromBody] RevokeTokenResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            _authenticationService.RevokeRefreshToken(resource.Token);
            return NoContent();
        }
    }
}