using System.Threading.Tasks;
using backend_api.Domain.Security.Hashing;
using backend_api.Domain.Security.Tokens;
using backend_api.Domain.Services;
using backend_api.Domain.Services.Communication;

namespace backend_api.Services
{
    /// <summary>
    /// The authentication service.
    /// Handles the creation of tokens.
    /// Has methods to create access tokens, get refresh tokens and revoke refresh tokens.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenHandler _tokenHandler;
        
        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="passwordHasher"></param>
        /// <param name="tokenHandler"></param>
        public AuthenticationService(IUserService userService, IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
            _passwordHasher = passwordHasher;
            _userService = userService;
        }

        /// <summary>
        /// Creates an access token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns>
        /// Unsuccessful TokenResponse if the credentials are invalid;
        /// Successful TokenRepsonse with the access token
        /// </returns>
        public async Task<TokenResponse> CreateAccessTokenAsync(string name, string password)
        {
            // check if the user exists and the password hash matches the password hash in the db
            var user = await _userService.GetUserByNameAsync(name);
            if (!user.Success || !_passwordHasher.PasswordMatches(password, user.User.Password))
                return new TokenResponse("Invalid credentials");

            // create the access token
            var token = _tokenHandler.CreateAccessToken(user.User);
            return new TokenResponse(token);
        }

        /// <summary>
        /// Refreshes an access token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="name"></param>
        /// <returns>
        /// Unsuccessful TokenResponse if refresh token is invalid;
        /// Unsuccessful TokenResponse if refresh token has expired;
        /// Unsuccessful TokenResponse if user does not exist;
        /// Successful TokenResponse with the access token
        /// </returns>
        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, string name)
        {
            // validate the refresh token
            var token = _tokenHandler.TakeRefreshToken(refreshToken);
            if (token == null)
                return new TokenResponse("Invalid refresh token");
            if (token.IsExpired())
                return new TokenResponse("Expired refresh token");

            // validate the username
            var userResult = await _userService.GetUserByNameAsync(name);
            if (!userResult.Success)
                return new TokenResponse("User not found");

            // create the new access token
            var accessToken = _tokenHandler.CreateAccessToken(userResult.User);
            return new TokenResponse(accessToken);
        }

        /// <summary>
        /// Remove the access token
        /// </summary>
        /// <param name="refreshToken"></param>
        public void RevokeRefreshToken(string refreshToken)
        {
            _tokenHandler.RevokeRefreshToken(refreshToken);
        }
    }
}