using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStay.WPF.DataAccessLayer.DAO;
using PrimeStay.WPF.DataAccessLayer.DTO;
using primestayWpf;
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
            IDao<CustomerDto> dao = DaoFactory.Create<CustomerDto>(_dataContext);
            (dao as IDaoAuth).SetToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsImlzcyI6ImV4YW1wbGUuY29tIiwiZXhwIjoxNjM3NTc2NTQ5LCJyb2xlIjoiYWRtaW4iLCJuYmYiOjE2Mzc1NzI5NDksImlhdCI6MTYzNzU3Mjk0OX0.sRN2UPPBdrcuqD_nOXa3pzaoQclmEWLagaWO4CFcI8U");

            //act
            var customers = dao.ReadAll(null);

            //assert
            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Any());
        }
    }
}