using System;

namespace backend_api.Domain.Security.Tokens
{
    /// <summary>
    /// Encoded token used to access api endpoints, that expire 30 seconds after creating
    /// </summary>
    public class AccessToken : JsonWebToken
    {
        public RefreshToken RefreshToken { get; private set; }

        public AccessToken(string token, long expiration, RefreshToken refreshToken) : base(token, expiration)
        {
            if(refreshToken == null)
                throw new ArgumentException("Specify a valid refresh token.");
                
            RefreshToken = refreshToken;
        }
    }
}