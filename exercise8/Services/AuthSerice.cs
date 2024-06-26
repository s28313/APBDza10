using kol2APBD.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using kol2APBD.Contexts;
using Microsoft.EntityFrameworkCore;

namespace kol2APBD.Services;

public interface IAuthService
    {
        Task<string> GenerateAccessToken(User user);
        Task<string> GenerateRefreshToken(User user);
        Task<bool> VerifyPassword(User user, string password);
        Task<User> AuthenticateUser(string login, string password);
        Task<string> RefreshAccessToken(string refreshToken);
    }

    public class AuthService : IAuthService
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> AuthenticateUser(string login, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Login == login);

            if (user == null || !await VerifyPassword(user, password))
                return null;

            return user;
        }

        public async Task<string> RefreshAccessToken(string refreshToken)
        {
            var storedRefreshToken = await _context.RefreshTokens.FindAsync(refreshToken);

            if (storedRefreshToken == null || storedRefreshToken.Expires < DateTime.UtcNow)
                return null;

            var user = await _context.Users.FindAsync(storedRefreshToken.UserId);

            if (user == null)
                return null;

            var accessToken = await GenerateAccessToken(user);
            return accessToken;
        }

        public async Task<bool> VerifyPassword(User user, string password)
        {
            byte[] salt = Convert.FromBase64String(user.PasswordHash);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return user.PasswordHash == hashed;
        }

        public async Task<string> GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Login)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                UserId = user.UserId,
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.UtcNow.AddDays(7)
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken.Token;
        }
    }