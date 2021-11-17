using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Model;
using PrimeStayApi.Services.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

/**
* Author: Lars Nysom
*/
namespace PrimeStayApi.Services
{
    public interface IAccountService
    {
        Userinfo Authenticate(string username, string password);
    }

    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDao<UserEntity> _dao;

        public AccountService(IConfiguration configuration, IDao<UserEntity> dao)
        {
            _configuration = configuration;
            _dao = dao;
        }

        public void Save(string username, string password)
        {
            var rngCSP = RNGCryptoServiceProvider.Create();

            byte[] randomSeq = new byte[256];
            rngCSP.GetNonZeroBytes(randomSeq);
            string salt = Convert.ToBase64String(randomSeq);

            string passwordHash = HashPassword(password, salt);

            _dao.Create(new UserEntity()
            {
                Username = username,
                PasswordHash = passwordHash,
                Salt = salt,
            });

        }

        private string HashPassword(string password, string salt)
        {
            HashAlgorithm hashAlgorithm = SHA256.Create();
            return Convert.ToBase64String(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(salt.Insert(salt.Length / 2, password))));
        }

        public Userinfo Authenticate(string username, string password)
        {
            UserEntity user = _dao.ReadAll(new UserEntity()
            {
                Username = username,
            }).SingleOrDefault();

            if (user is null) return null;

            string passwordHash = HashPassword(password, user.Salt);
            if (!user.PasswordHash.Equals(passwordHash)) throw new Exception("Error, incorrect password");
            return CreateAuthenticatedUser(username);
        }

        private Userinfo CreateAuthenticatedUser(string username)
        {
            var expires = DateTime.Now.AddDays(1);

            var userInfo = new Userinfo
            {
                IsAuthenticated = true,
                Username = username,
                Expires = expires,
            };
            userInfo.Token = GenerateJwt(userInfo, expires);
            return userInfo;
        }

        private string GenerateJwt(Userinfo userinfo, DateTime expires)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.Username),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JwtSettings:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, expires.ToString()),
            };

            var identity = new ClaimsIdentity(claims, "Token");
            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var handler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            };
            return handler.CreateEncodedJwt(descriptor);
        }
    }
}
