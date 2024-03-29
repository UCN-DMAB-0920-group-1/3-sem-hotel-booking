﻿using DataAccessLayer;
using DataAccessLayer.DTO;
using RestSharp;
using System.Collections.Generic;

namespace DataAccessLayer.DAO
{
    internal class HotelDao : BaseDao<IDataContext<IRestClient>>, IDao<HotelDto>
    {
        public HotelDao(IDataContext<IRestClient> dataContext) : base(dataContext)
        {
        }

        public string Create(HotelDto model)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(HotelDto model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<HotelDto> ReadAll(HotelDto model)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest("/api/hotel?active=true", Method.GET, DataFormat.Json);
            IRestResponse<IEnumerable<HotelDto>> restResponse = restClient.Get<IEnumerable<HotelDto>>(restRequest);
            return restResponse.Data;
        }

        public HotelDto ReadByHref(string href)
        {
            IRestClient restClient = DataContext.Open();
            IRestRequest restRequest = new RestRequest(href, Method.GET, DataFormat.Json);
            return restClient.Get<HotelDto>(restRequest).Data;
        }

        public int Update(HotelDto model)
        {
            throw new System.NotImplementedException();
        }
    }
}