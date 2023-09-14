using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Test_Project2.Data_Contest;
using Test_Project2.Models.UserModel;

namespace Test_Project2.Repository.UserRepository
{
    public class Token_Handler : IToken_Handler
    {
        private readonly IConfiguration configuration;
        private readonly Book_Shop_DataContext user_Context;
        public Token_Handler(IConfiguration configuration, Book_Shop_DataContext user_Context)
        {
            this.configuration = configuration;
            this.user_Context = user_Context;
        }
        public Task<string> CreateTokenAsync(General_User users)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

            // create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, users.SurName!),
                new Claim(ClaimTypes.Surname, users.FirstName!),
                new Claim(ClaimTypes.Email, users.Email!)
            };
            //loop into roles of usres

            users.Roles!.ForEach((role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
               issuer: configuration["Jwt:Issuer"],
               audience: configuration["Jwt:Audience"],
               claims: claims,
               expires: DateTime.Now.AddHours(3),
               signingCredentials: credentials
                );
            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<string> RefreshTokenAsync(string email)
        {
            var users = await user_Context.General_User!.FirstOrDefaultAsync(u => u.Email == email);
            if (users == null)
            {
#pragma warning disable CS8603 // Possible null reference return.
                return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
            var newRefreshToken = GenerateRefreshToken();
            _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            users.RefreshToken = newRefreshToken;
            users.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
            await user_Context.SaveChangesAsync();
            return users.RefreshToken;
        }


        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}

