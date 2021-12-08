using DataAccessLayer.DTO;
using Models;
using RestSharp;
using System;
using System.Collections.Generic;


namespace DataAccessLayer.DAO
{
    internal class AccountDao : BaseDao<IDataContext<IRestClient>>, IDao<UserDto>, IDaoAccountExtension<LoginResponse>
    {
       
        public AccountDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
            
        }

        public LoginResponse Authorize(string username, string password)
        {
            IRestClient client = DataContext.Open();
            IRestRequest request = new RestRequest("/api/account/login", Method.POST, DataFormat.Json).AddJsonBody(username, password);
            var res = client.Execute<LoginResponse>(request);
            return res.Data;
        }

        public string Create(UserDto model)
        {
            throw new NotImplementedException();
        }

        public int Delete(UserDto model)
        {
            throw new NotImplementedException();
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
}
