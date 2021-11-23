using Microsoft.VisualStudio.TestTools.UnitTesting;
using primestayWpf;
using primestayWpf.src.auth;
using PrimestayWPF.DataAccessLayer;
using PrimestayWPF.DataAccessLayer.DAO;
using PrimestayWPF.DataAccessLayer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace primestayWpf.Test
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
            var customers = dao.ReadAll(null, token);

            //assert
            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Any());
        }
    }
}