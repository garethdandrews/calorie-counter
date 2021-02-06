using backend_api.Domain.Models;

namespace backend_api.Domain.Security.Tokens
{
    /// <summary>
    /// The token handler.
    /// Has methods to create access tokens, get refresh tokens and revoke refresh tokens
    /// </summary>
    public interface ITokenHandler
    {
        /// <summary>
        /// Create an access token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        AccessToken CreateAccessToken(User user);

        /// <summary>
        /// Checks if the refresh token exists and removes it
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        /// null if the token is empty
        /// the refresh token
        /// </returns>
        RefreshToken TakeRefreshToken(string token);

        /// <summary>
        /// Remove the refresh token
        /// </summary>
        /// <param name="token"></param>
        void RevokeRefreshToken(string token);
    }
}