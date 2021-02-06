using System.Threading.Tasks;
using backend_api.Domain.Services.Communication;

namespace backend_api.Domain.Services
{
    /// <summary>
    /// The authentication service
    /// Handles the creation of tokens
    /// Has methods to create access tokens, get refresh tokens and revoke refresh tokens
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Creates an access token
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns>
        /// Unsuccessful TokenResponse if the credentials are invalid;
        /// Successful TokenRepsonse with the access token
        /// </returns>
        Task<TokenResponse> CreateAccessTokenAsync(string name, string password);

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
         Task<TokenResponse> RefreshTokenAsync(string refreshToken, string name);

         /// <summary>
        /// Remove the access token
        /// </summary>
        /// <param name="refreshToken"></param>
         void RevokeRefreshToken(string refreshToken);
    }
}