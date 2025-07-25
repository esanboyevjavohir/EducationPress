﻿using EduPress.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EduPress.Application.Helpers.GenerateJWT
{
    public class JwtTokenHandler : IJwtTokenHandler
    {
        public readonly JwtOption jwtOption;
        public JwtTokenHandler(IOptions<JwtOption> options)
        {
            jwtOption = options.Value;
        }
        public string GenerateAccessToken(User user)
        {
            var claim = new List<Claim>
            {
                new Claim(CustomClaimNames.Id, user.Id.ToString()),
                new Claim(CustomClaimNames.Email, user.Email),
                new Claim(CustomClaimNames.Role, user.Role.ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this.jwtOption.SecretKey));

            var token = new JwtSecurityToken(
                issuer: this.jwtOption.Issuer,
                audience: this.jwtOption.Audience,
                expires: DateTime.Now.AddMinutes(this.jwtOption.ExpirationInMinutes),
                claims: claim,
                signingCredentials: new SigningCredentials(
                    key: authSigningKey,
                    algorithm: SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateAccessToken(User user, string sessionToken)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimNames.Id , user.Id.ToString()),
                new Claim(CustomClaimNames.Email, user.Email),
                new Claim(CustomClaimNames.Role , user.Role.ToString()),
                new Claim(CustomClaimNames.Token, sessionToken)
            };

            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this.jwtOption.SecretKey));

            var jwtToken = new JwtSecurityToken(
                issuer: this.jwtOption.Issuer,
                audience: this.jwtOption.Audience,
                expires: DateTime.UtcNow.AddMinutes(this.jwtOption.ExpirationInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(
                 key: authSigningKey,
                 algorithm: SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public string GenerateRefreshToken()
        {
            byte[] bytes = new byte[64];

            using var randomGenerator = RandomNumberGenerator.Create();

            randomGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
