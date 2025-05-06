using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task02.Core.Service.Contracts;

namespace Task02.Service.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateToken(Core.Entities.User user, UserManager<Core.Entities.User> userManager)
        {
            var authClaim = new List<Claim>() 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Name)
            };

            var userRole = await userManager.GetRolesAsync(user);
            foreach (var role in userRole)
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role));
            }

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            var token = new JwtSecurityToken
                (
                    issuer : _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                    claims:authClaim,
                    signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)

                ) ;

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
