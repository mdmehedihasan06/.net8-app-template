using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Infrastructure.Helper
{
    public class JwtTokenUtility
    {
        private readonly string _issuer = "localhost";
        private readonly string _audience = "client";
        private readonly string _accessTokenSecret = "SLSECRETKEY2024USBGROUP_SLSECRETKEY2024USBGROUP";
        private readonly string _refreshTokenSecret = "SLSECRETKEY2024USBGROUP_SLSECRETKEY2024USBGROUP";

        public JwtTokenUtility(string issuer, string audience, string accessTokenSecret, string refreshTokenSecret)
        {
            _issuer = issuer;
            _audience = audience;
            _accessTokenSecret = accessTokenSecret;
            _refreshTokenSecret = refreshTokenSecret;
        }
        public JwtTokenUtility() { }

        public (string accessToken, string refreshToken) GenerateTokens(string userId, string username)
        {            
            // Generate Access Token
            var accessToken = GenerateAccessToken(userId, username);

            // Generate Refresh Token
            var refreshToken = GenerateRefreshToken();

            return (accessToken, refreshToken);
        }

        private string GenerateAccessToken(string userId, string username)
        {
            var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_accessTokenSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var accessToken = new JwtSecurityToken(
                _issuer,
                _audience,
                claims,
                expires: DateTime.Now.AddHours(12), // Adjust the expiration as needed
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private string GenerateRefreshToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_refreshTokenSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var refreshToken = new JwtSecurityToken(
                _issuer,
                _audience,
                expires: DateTime.Now.AddDays(2), // Refresh token may have a longer expiration
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }

        public static string GenerateJwtToken(string userId, string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                // Add other custom claims as needed
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SLSECRETKEY2024USBGROUP_SLSECRETKEY2024USBGROUP"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "localhost",
                audience: "client",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
