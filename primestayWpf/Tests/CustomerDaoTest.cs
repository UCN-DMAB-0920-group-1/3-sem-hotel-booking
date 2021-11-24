using DataAccessLayer;
using DataAccessLayer.DAO;
using DataAccessLayer.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Linq;
using WinApp;
using WinApp.src.auth;

namespace Test
{
    [TestClass()]
    public class CustomerDaoTest
    {
        private static IDataContext<IRestClient> _dataContext = RestDataContext.GetInstance();

        [TestMethod()]
        public void ReadAllTest()
        {
            //assign
            IDao<UserDto> userDao = DaoFactory.Create<UserDto>(_dataContext, Auth.AccessToken);
            var token = (userDao as IDaoAuthExtension<UserDto>).login("admin", "admin").Token;

            IDao<CustomerDto> dao = DaoFactory.Create<CustomerDto>(_dataContext, Auth.AccessToken);

            //act
            var customers = dao.ReadAll(null);

            //assert
            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Any());
        }
    }
}