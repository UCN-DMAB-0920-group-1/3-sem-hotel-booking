﻿using PrimeStay.Model;
using RestSharp;
using System;
using System.Data;

namespace PrimeStay.DataAccessLayer.DAO
{
    public static class DaoFactory
    {
        public static IDao<T> Create<T>(IDataContext dataContext)
        {
            Type DataContextType = dataContext.GetType(); // throws exeption if datacontext is null

            if (typeof(IDataContext<IRestClient>).IsAssignableFrom(DataContextType))
            {
                return typeof(T) switch
                {
                    var dao when dao == typeof(Hotel) => new REST.HotelDao(dataContext as IDataContext<IRestClient>) as IDao<T>,
                    var dao when dao == typeof(Room) => new REST.RoomDao(dataContext as IDataContext<IRestClient>) as IDao<T>,
                    var dao when dao == typeof(Location) => new REST.LocationDao(dataContext as IDataContext<IRestClient>) as IDao<T>,
                    _ => null,
                };
            }

            else if (typeof(IDataContext<IDbConnection>).IsAssignableFrom(DataContextType))
            {
                return typeof(T) switch
                {
                    var dao when dao == typeof(Hotel) => new SQL.HotelDao(dataContext as IDataContext<IRestClient>) as IDao<T>,
                    _ => null,
                };
            }

            throw new DaoFactoryException("DataContext type not known");
        }
    }
}