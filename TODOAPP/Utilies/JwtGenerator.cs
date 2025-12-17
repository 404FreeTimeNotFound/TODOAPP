using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TODOAPP.Configurations;
using TODOAPP.Controllers.Data;
using TODOAPP.Data.Services;
using TODOAPP.Models;
using TODOAPP.Models.DTOs.Responses;

namespace TODOAPP.Validators
{
    public  class JwtGenerator:IJwtGenerator
    {
        private readonly JwtConfig _jwtConfig;
        private readonly ApiDbContext _context;
        public JwtGenerator(IOptionsMonitor<JwtConfig> jwtConfig, ApiDbContext context)
        {
            _jwtConfig = jwtConfig.CurrentValue;
            _context = context;
        }
        public async Task<AuthResult> GenerateToken(IdentityUser user)
        {
            var key=Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenHandler= new JwtSecurityTokenHandler();
            var tokenDescriptor=new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(new[]
                {
                    new Claim("Id",user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                }),
                Expires=DateTime.UtcNow.AddSeconds(_jwtConfig.ExpiryDuration),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)};
                var token =tokenHandler.CreateToken(tokenDescriptor);
                var refreshToken=new RefreshToken
                {
                    JwtId=token.Id,
                    UserId=user.Id,
                    IsRevoked=false,
                    CreationDate=DateTime.UtcNow,
                    ExpirationDate=DateTime.UtcNow.AddDays(15),
                    Token= await GenerateRefreshToken()
                };
                await _context.RefreshTokens.AddAsync(refreshToken);
                await _context.SaveChangesAsync();
                 return new AuthResult
                 {
                    Success=true,
                    RefreshToken= refreshToken.Token,
                    Token= tokenHandler.WriteToken(token)
                 };
            }
    private async Task<string> GenerateRefreshToken()
        {
            var randomeNumber= new byte[64];
            using var rang=RandomNumberGenerator.Create();
            rang.GetBytes(randomeNumber);
            return Convert.ToBase64String(randomeNumber);
        }

    }
}