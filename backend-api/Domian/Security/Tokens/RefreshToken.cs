namespace backend_api.Domain.Security.Tokens
{
    /// <summary>
    /// Refresh token, an encoded token that expires 900 seconds after token is created
    /// </summary>
    public class RefreshToken : JsonWebToken
    {
        public RefreshToken(string token, long expiration) : base(token, expiration)
        {
        }
    }
}