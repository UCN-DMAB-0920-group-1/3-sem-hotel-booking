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
        Userinfo Save(string username, string password, string role);
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

        public Userinfo Save(string username, string password, string role)
        {
            var rngCSP = RNGCryptoServiceProvider.Create();

            byte[] randomSeq = new byte[256];
            rngCSP.GetNonZeroBytes(randomSeq);
            string salt = Convert.ToBase64String(randomSeq);

            string passwordHash = HashPassword(password, salt);

            try
            {
                _dao.Create(new UserEntity()
                {
                    Username = username,
                    Password = passwordHash,
                    Role = role,
                    Salt = salt,
                });

                return CreateAuthenticatedUser(username, role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
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

            if (user is null)
            {
                string passwordHash = HashPassword(password, "salt");
                return null;
            }
            else
            {
                string passwordHash = HashPassword(password, user.Salt);
                if (!user.Password.Equals(passwordHash)) return null;

                return user switch
                {
                    _ when user.Role.Equals("admin") => CreateAuthenticatedUser(username, "admin"),
                    _ when user.Role.Equals("user") => CreateAuthenticatedUser(username, "user"),
                    _ => throw new Exception("Unkown role type")
                };
            }
        }

        private Userinfo CreateAuthenticatedUser(string username, string role)
        {
            var expires = DateTime.Now.AddDays(1);

            var userInfo = new Userinfo
            {
                IsAuthenticated = true,
                Username = username,
                Expires = expires,
            };
            userInfo.Token = GenerateJwt(userInfo, expires, role);
            return userInfo;
        }

        private string GenerateJwt(Userinfo userinfo, DateTime expires, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.Username),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JwtSettings:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, expires.ToString()),
            };

            var identity = new ClaimsIdentity(claims, "Token");
            identity.AddClaim(new Claim(ClaimTypes.Role, role));

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
