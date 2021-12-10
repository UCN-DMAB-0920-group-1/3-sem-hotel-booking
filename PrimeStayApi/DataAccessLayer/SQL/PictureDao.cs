using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace DataAccessLayer.SQL
{
    internal class PictureDao : BaseDao<IDataContext<IDbConnection>>, IDao<PictureEntity>
    {
        #region SQL-Queries
        private readonly static string SELECT_ALL_PICTURES = $"SELECT * FROM TablePictures " +
                                                    $"INNER JOIN picture ON picture.id = TablePictures.picture_id ";
        #endregion

        public PictureDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public int Create(PictureEntity model)
        {
            throw new System.NotImplementedException();
        }

        public int Delete(PictureEntity model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PictureEntity> ReadAll(PictureEntity model)
        {
            using IDbConnection connection = DataContext.Open();

            string whereStatement = GetWhereStatement(model);
            return connection.Query<PictureEntity>(SELECT_ALL_PICTURES + whereStatement, model);
        }

        public PictureEntity ReadById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Update(PictureEntity model)
        {
            throw new System.NotImplementedException();
        }

        private static string GetWhereStatement(PictureEntity model)
        {
            return model.Type switch
            {
                string type when type.Equals("hotel") => $"WHERE type = @Type AND hotel_id = @Hotel_id",
                string type when type.Equals("room") => $"WHERE type = @Type AND room_type_id = @Room_type_id",
                string type => throw new DaoException($"Invalid type: {type}"),
            };
        }
    }
}
