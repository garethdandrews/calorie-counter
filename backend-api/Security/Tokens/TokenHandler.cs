using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using backend_api.Domain.Models;
using backend_api.Domain.Security.Hashing;
using backend_api.Domain.Security.Tokens;
using Microsoft.Extensions.Options;

namespace backend_api.Security.Tokens
{
    /// <summary>
    /// The token handler.
    /// Has methods to create access tokens, get refresh tokens and revoke refresh tokens
    /// </summary>
    public class TokenHandler : ITokenHandler
    {
        private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();
        private readonly TokenOptions _tokenOptions;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly IPasswordHasher _passwordHasher;

        /// <summary>
        /// Handles dependencies
        /// </summary>
        /// <param name="tokenOptionsSnapshot"></param>
        /// <param name="signingConfigurations"></param>
        /// <param name="passwordHasher"></param>
        public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot, SigningConfigurations signingConfigurations, IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _tokenOptions = tokenOptionsSnapshot.Value;
            _signingConfigurations = signingConfigurations;
        }

        /// <summary>
        /// Create an access token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public AccessToken CreateAccessToken(User user)
        {
            var refreshToken = BuildRefreshToken();
            var accessToken = BuildAccessToken(user, refreshToken);
            _refreshTokens.Add(refreshToken);

            return accessToken;
        }

        /// <summary>
        /// Checks if the refresh token exists and removes it
        /// </summary>
        /// <param name="token"></param>
        /// <returns>
        /// null if the token is empty
        /// the refresh token
        /// </returns>
        public RefreshToken TakeRefreshToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            // check if the refresh token exists before removing it
            var refreshToken = _refreshTokens.SingleOrDefault(t => t.Token == token);
            if (refreshToken != null)
                _refreshTokens.Remove(refreshToken);

            return refreshToken;
        }

        /// <summary>
        /// Remove the refresh token
        /// </summary>
        /// <param name="token"></param>
        public void RevokeRefreshToken(string token)
        {
            TakeRefreshToken(token);
        }

        /// <summary>
        /// Generates a refresh token
        /// </summary>
        /// <returns>
        /// A refresh token
        /// </returns>
        private RefreshToken BuildRefreshToken()
        {
            var refreshToken = new RefreshToken
            (
                token : _passwordHasher.HashPassword(Guid.NewGuid().ToString()), // random hash
                expiration : DateTime.UtcNow.AddSeconds(_tokenOptions.RefreshTokenExpiration).Ticks // a date higher than the access token expiration date
            );

            return refreshToken;
        }

        /// <summary>
        /// Generates an access token and creates an encoded string to represent the JSON object.
        /// Uses JwtSecurityToken and JwtSecurityTokenHandler.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="refreshToken"></param>
        /// <returns>
        /// An access token
        /// </returns>
        private AccessToken BuildAccessToken(User user, RefreshToken refreshToken)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

            // Generate an access token
            var securityToken = new JwtSecurityToken
            (
                issuer : _tokenOptions.Issuer,
                audience : _tokenOptions.Audience,
                claims : GetClaims(user),
                expires : accessTokenExpiration,
                notBefore : DateTime.UtcNow,
                signingCredentials : _signingConfigurations.SigningCredentials
            );
    
            // Create an encoded string to represent the JSON object
            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
        }

        /// <summary>
        /// Generates claims for the token identifier, the username and the permissions
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// A list of the users claims
        /// </returns>
        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name)
            };

            foreach (var userRole in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
            }

            return claims;
        }
    }
}