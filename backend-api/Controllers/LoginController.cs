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
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        [Route("/login")]
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