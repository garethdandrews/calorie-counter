using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace backend_api.Security.Tokens
{
    public class SigningConfigurations
    {        
        public SecurityKey SecurityKey { get; } // A 2048 bits security key for validating token signatures
        public SigningCredentials SigningCredentials { get; } // The token credentials, generated using the security key and the RSA SHA256 algorithm

        public SigningConfigurations(string key)
        {
            var keyBytes = Encoding.ASCII.GetBytes(key);

            SecurityKey = new SymmetricSecurityKey(keyBytes);
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}