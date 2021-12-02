using DataAccessLayer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAO
{
    class PriceDao : BaseDao<IDataContext<IRestClient>>, IDao<PriceDto>
    {
        public PriceDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(PriceDto model)
        {
            throw new NotImplementedException();
        }

        public int Delete(PriceDto model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PriceDto> ReadAll(PriceDto model)
        {
            
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest($"/api/price", Method.GET, DataFormat.Json);
            restRequest.AddQueryParameter("roomTypeId", model.RoomTypeId.ToString());

            IRestResponse<IEnumerable<PriceDto>> restResponse = restClient.Get<IEnumerable<PriceDto>>(restRequest);
            var res = restResponse.Data;

            return res;
        }

        public PriceDto ReadByHref(string href)
        {
            throw new NotImplementedException();
        }

        public int Update(PriceDto model)
        {
            throw new NotImplementedException();
        }
    }
}
