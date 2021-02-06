using System.Threading.Tasks;
using backend_api.Domain.Services.Communication;

namespace backend_api.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> CreateAccessTokenAsync(string name, string password);
         Task<TokenResponse> RefreshTokenAsync(string refreshToken, string name);
         void RevokeRefreshToken(string refreshToken);
    }
}