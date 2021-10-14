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
using priberam.Models.DTO;

namespace priberam.Services
{
    public class IdentityService : IdentityServiceInterface
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Expires { get; set; }
        private string SecretKey { get; set; }

        public IdentityService(
            IConfiguration configuration,
            SignInManager<ApplicationUser> signInManager
        )
        {
            _configuration = configuration;
            _signInManager = signInManager;

            SecretKey = _configuration.GetValue<string>("Security:Secret");
            Issuer = _configuration.GetValue<string>("Security:Issuer");   
            Audience = _configuration.GetValue<string>("Security:Audience");       
            Expires = int.Parse(_configuration.GetValue<string>("Security:Expires")); 
        }
        public AccountToken BuildToken(AccountUser User)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, User.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(this.SecretKey) );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(this.Expires);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: this.Issuer,
               audience: this.Audience,
               claims: claims,
               expires: expiration,
               signingCredentials: creds
            );

            return new AccountToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration.ToString()
            };
        }

        public async Task<bool> isValidAccount(AccountUser User)
        {
            var result = await _signInManager.PasswordSignInAsync(User.Email, User.Password, isPersistent: false, lockoutOnFailure: false);
            return result.Succeeded;
        }
    }
}
