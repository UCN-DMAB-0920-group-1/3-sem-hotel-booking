using PrimeStay.WPF.DataAccessLayer.DTO;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace PrimeStay.WPF.DataAccessLayer.DAO

{
    internal class UserDao : BaseDao<IDataContext<IRestClient>>, IDao<UserDto>, IDaoAuthExtension<UserDto>
    {
        public UserDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(UserDto model)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(UserDto model)
        {
            throw new System.NotImplementedException();
        }

        public UserDto login(string username, string password)
        {
            Dictionary<string, string> body = new Dictionary<string, string>()
            {
                {"username",username },
                {"password", password },
            };

            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/account/login", Method.POST, DataFormat.Json);
            restRequest.AddJsonBody(body);

            var res = restClient.Post<UserDto>(restRequest).Data;
            return res;
        }

        public IEnumerable<UserDto> ReadAll(UserDto model)
        {
            throw new System.NotImplementedException();
        }

        public UserDto ReadByHref(string href)
        {
            throw new System.NotImplementedException();
        }

        public int Update(UserDto model)
        {
            throw new System.NotImplementedException();
        }
    }
}