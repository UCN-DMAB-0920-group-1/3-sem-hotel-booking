using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf.Forms;
using primestayWpf.src.auth;
using System;
using System.Collections.Generic;
using System.Windows;

namespace primestayWpf.Test
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


        [TestMethod()]
        public void TestInvalidLoginButtonAuthWindow()
        {
            //arrange
            IDao<UserDto> dao = new MockUserDao();
            var window = new AuthWindow(dao);
            window.Show();

            string username = "1234";
            string password = "ytrewq";

            //Act


            Application.Current.Dispatcher.Invoke(delegate
            {
                window.usernameField.Text = username;
                window.passwordField.Password = password;
                window.loginBtn_Click(this, null); //Mock user input
            });

            //Assert
            Assert.AreEqual("Username or password is invalid!", window.errorLabel.Content);
            Assert.AreEqual("", Auth.AccessToken);
        }

        [TestMethod()]
        public void TestInvalidInputLoginButtonAuthWindow()
        {

            //arrange
            IDao<UserDto> dao = new MockUserDao();
            var window = new AuthWindow(dao);

            //Act
            window.usernameField.Text = "";
            window.passwordField.Password = "";
            window.loginBtn_Click(this, null); //Mock user input

            //Assert
            Assert.AreEqual("Both username and password must be set!", window.errorLabel.Content as string);
            Assert.AreEqual("", Auth.AccessToken);
        }

        [TestMethod()]
        public void TestValidLoginButtonAuthWindow()
        {
            //arrange
            IDao<UserDto> dao = new MockUserDao();
            var window = new AuthWindow(dao);

            string username = "1234";
            string password = "qwerty";


            //Act
            window.usernameField.Text = username;
            window.passwordField.Password = password;
            window.loginBtn_Click(this, null); //Mock user input

            //Assert
            Assert.AreEqual("", window.errorLabel.Content);
            Assert.AreEqual("valid_token", Auth.AccessToken);
            Assert.AreEqual("1234", Auth.username);
        }
    }


    #region mock implementations
    //IDao<UserDto> left out, due to the test not needing it
    internal class MockUserDao : IDao<UserDto>, IDaoAuthExtension<UserDto>
    {
        public string Create(UserDto model, string token = null)
        {
            throw new NotImplementedException();
        }

        public int Delete(UserDto model, string token = null)
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

        public int Update(UserDto model, string token = null)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

}

