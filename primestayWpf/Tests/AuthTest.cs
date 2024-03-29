﻿using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Test
{
    [TestClass()]
    public class AuthTest
    {
        [TestMethod()]
        public void TestDaoLogin()
        {
            //arrange
            IDao<UserDto> dao = new MockUserDao();

            //Act
            var invalidLogin = (dao as IDaoAuthExtension<UserDto>).login("invalid_username", "invalid_password");
            var validLogin = (dao as IDaoAuthExtension<UserDto>).login("1234", "qwerty");

            //Assert
            Assert.IsNull(invalidLogin);
            Assert.IsNotNull(validLogin);
            Assert.IsTrue(validLogin.Expires.ToString() == DateTime.Now.AddMonths(12).ToString());
            Assert.AreEqual(validLogin.name, "1234");
            Assert.AreEqual(validLogin.Token, "valid_token");
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
            if (username == "1234" && password == "qwerty")
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

