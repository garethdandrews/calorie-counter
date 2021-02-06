using System;

namespace backend_api.Domain.Security.Tokens
{
    /// <summary>
    /// Access tokens contain:
    ///     - encoded token used to access api endpoints, that expire 30 seconds after creating,
    ///     - refresh token, an encoded token that expires 900 seconds after token is created,
    ///     - expiration date of the access token for validation,
    /// </summary>
    public abstract class JsonWebToken
    {
        public string Token { get; protected set; }
        public long Expiration { get; protected set; }

        public JsonWebToken(string token, long expiration)
        {
            if(string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Invalid token.");

            if(expiration <= 0)
                throw new ArgumentException("Invalid expiration.");

            Token = token;
            Expiration = expiration;
        }

        public bool IsExpired() => DateTime.UtcNow.Ticks > Expiration;
    }
}