using MVC.DataAccessLayer.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVC.DataAccessLayer.DAO
{
    internal class RoomDao : BaseDao<IDataContext<IRestClient>>, IDao<RoomDto>
    {
        public RoomDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public int Create(RoomDto model)
        {
            throw new NotImplementedException();
        }

        public int Delete(RoomDto model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RoomDto> ReadAll(RoomDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest($"/api/room/", Method.GET, DataFormat.Json);
            var res = restClient.Get<IEnumerable<RoomDto>>(restRequest).Data;
            return res.Where(r => r.Hotel_Id == model.Hotel_Id);
        }

        public RoomDto ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(RoomDto model)
        {
            throw new NotImplementedException();
        }
    }
}
