using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using PrimeStay.WPF;
using primestayWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace primestayWpf.Test
{
    [TestClass()]
    public class AuthTest
    {


        [TestMethod()]
        public void TestDaoLogin()
        {
            //arrange
            IDataContext _context = RestDataContext.GetInstance();
            IDao<UserDto> dao = new MockUserDao();

            var res = (dao as IDaoAuthExtension<UserDto>).login("invalid_username", "invalid_password");
            Assert.Fail();
        }


        [TestMethod()]
        public void TestLoginButtonAuthScreenWrongInfo()
        {

            Assert.Fail();
        }

        [TestMethod()]
        public void TestLoginButtonAuthScreenCorrectInfo()
        {

            Assert.Fail();
        }
    }


    #region mock implementations
    //IDao<UserDto> left out, due to the test not needing it
    internal class MockUserDao : IDao<UserDto>, IDaoAuthExtension<UserDto>
    {
        public string Create(UserDto model)
        {
            throw new NotImplementedException();
        }

        public int Delete(UserDto model)
        {
            throw new NotImplementedException();
        }

        public UserDto login(string username, string password)
        {
            if(username == "1234" && password == "qwerty")
            {
                return new UserDto()
                {
                    Expires = DateTime.Now.AddMonths(12),
                    Href = "",
                    name = "1234",
                    Token = "valid_token",
                };
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<UserDto> ReadAll(UserDto model)
        {
            throw new NotImplementedException();
        }

        public UserDto ReadByHref(string href)
        {
            throw new NotImplementedException();
        }

        public int Update(UserDto model)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

}

