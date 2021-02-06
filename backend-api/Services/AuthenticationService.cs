using System.Threading.Tasks;
using backend_api.Domain.Security.Hashing;
using backend_api.Domain.Security.Tokens;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;

namespace backend_api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;
        
        public AuthenticationService(IUserService userService, IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _passwordHasher = passwordHasher;
            _userService = userService;
        }

        public async Task<TokenResponse> CreateAccessTokenAsync(string name, string password)
        {
            var user = await _userService.GetUserByNameAsync(name);

            if (user == null || !_passwordHasher.PasswordMatches(password, user.Password))
                return new TokenResponse("Invalid credentials");

            var token = _tokenHandler.CreateAccessToken(user);

            return new TokenResponse(token);
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string name)
        {
            var token = _tokenHandler.TakeRefreshToken(refreshToken);

            if (token == null)
            {
                return new TokenResponse("Invalid refresh token");
            }

            if (token.IsExpired())
            {
                return new TokenResponse("Expired refresh token");
            }

            var user = await _userService.GetUserByNameAsync(name);
            if (user == null)
            {
                return new TokenResponse("Invalid refresh token");
            }

            var accessToken = _tokenHandler.CreateAccessToken(user);
            return new TokenResponse(accessToken);
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            _tokenHandler.RevokeRefreshToken(refreshToken);
        }
    }
}