using backend_api.Domain.Models;
using backend_api.Domain.Security.Tokens.Security.Tokens;

namespace backend_api.Domain.Security.Tokens
{
    public interface ITokenHandler
    {
         AccessToken CreateAccessToken(User user);
         RefreshToken TakeRefreshToken(string token);
         void RevokeRefreshToken(string token);
    }
}