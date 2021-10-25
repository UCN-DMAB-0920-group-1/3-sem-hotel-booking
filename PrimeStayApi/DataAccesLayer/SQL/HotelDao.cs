﻿using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class HotelDao : BaseDao<IDataContext>, IDao<HotelEntity>
    {

        public HotelDao(IDataContext dataContext) : base(dataContext)
        {
        }

        public int Create(HotelEntity model)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(HotelEntity model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<HotelEntity> ReadAll(HotelEntity model)
        {
            model.Name = model.Name != null ? "%" + model.Name + "%" : null;
            model.Description = model.Description != null ? "%" + model.Description + "%" : null;
            model.Staffed_hours = model.Staffed_hours != null ? "%" + model.Staffed_hours + "%" : null;

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<HotelEntity>($"SELECT * FROM Hotel WHERE " +
                                                                 $"id=ISNULL(@id,id)" +
                                                                 $"AND name LIKE ISNULL(@name,name)" +
                                                                 $"AND description LIKE ISNULL(@description,description)" +
                                                                 $"AND staffed_hours LIKE ISNULL(@staffed_hours,staffed_hours)" +
                                                                 $"AND stars = ISNULL(@stars,stars)",
                                                                 new { model.Id, model.Name, model.Description, model.Staffed_hours, model.Stars });

            };
        }

        public HotelEntity ReadById(int id)
        {

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.QueryFirst<HotelEntity>($@"Select * FROM Hotel WHERE ID = @id", new { id });

            };
        }

        public int Update(HotelEntity model)
        {
            throw new System.NotImplementedException();
        }
    }
}