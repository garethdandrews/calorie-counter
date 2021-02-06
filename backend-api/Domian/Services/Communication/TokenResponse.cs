
using backend_api.Domain.Security.Tokens.Security.Tokens;

namespace backend_api.Domain.Services.Communication
{
    public class TokenResponse : BaseResponse
    {
        public AccessToken AccessToken { get; private set; }

        private TokenResponse(bool success, string message, AccessToken accessToken) : base(success, message)
        {
            AccessToken = accessToken;
        }

        // Creates a success response
        public TokenResponse(AccessToken accessToken) : this(true, string.Empty, accessToken)
        {
        }

        // Creates an error response
        public TokenResponse(string message) : this(false, message, null)
        {
        }
    }
}