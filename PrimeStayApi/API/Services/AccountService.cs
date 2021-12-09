using API.Services.Models;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Services
{
    /**
     * Author: Julius, Magnus, Michael, Mike, Nicolas
     * Origin: Lars Nysom
     * <summary>An interface for an account service. Handles user login/registration</summary>
     */
    public interface IAccountService
    {
        Userinfo Save(string username, string password, string role);
        Userinfo Authenticate(string username, string password);
    }

    /**
     * Author: Julius, Magnus, Michael, Mike, Nicolas
     * Origin: Lars Nysom
     * <summary>An implementation of <see cref="IAccountService"/></summary>
     */
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDao<UserEntity> _dao;
        private readonly IDao<CustomerEntity> _customerDao;

        public AccountService(IConfiguration configuration, IDao<UserEntity> dao, IDao<CustomerEntity> customerDao)
        {
            _configuration = configuration;
            _dao = dao;
            _customerDao = customerDao;
        }

        public Userinfo Save(string username, string password, string role)
        {
            var rngCSP = RandomNumberGenerator.Create();

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
            string saltedPassword = salt.Insert(salt.Length / 2, password);
            return Convert.ToBase64String(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword)));
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

                return CreateAuthenticatedUser(username, user.Role, user.Id ?? -1);
            }
        }

        private Userinfo CreateAuthenticatedUser(string username, string role, int id = -1)
        {
            var expires = DateTime.Now.AddDays(1);

            var userInfo = new Userinfo
            {
                IsAuthenticated = true,
                Username = username,
                Expires = expires,
            };
            if (id != -1) userInfo.CustomerId = _customerDao.ReadAll(new CustomerEntity() { User_id = id }).FirstOrDefault().Id;
            userInfo.Token = GenerateJwt(userInfo, expires, role);
            return userInfo;
        }

        private string GenerateJwt(Userinfo userinfo, DateTime expires, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["JwtSettings:Issuer"]),
                new Claim(JwtRegisteredClaimNames.Exp, expires.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, userinfo.Username),
                new Claim("customerId", userinfo.CustomerId.ToString()),
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
