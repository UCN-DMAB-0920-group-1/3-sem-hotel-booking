using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
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
            string whereStatement = GetWhereStatement(model);

            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<PictureEntity>(SELECT_ALL_PICTURES + whereStatement, model);

            };
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
            string whereStatement;
            switch (model.Type)
            {

                case "hotel":
                    whereStatement = $"WHERE type = @Type AND hotel_id = @Hotel_id";
                    break;
                case "room":
                    whereStatement = $"WHERE type = @Type AND room_type_id = @Room_id";
                    break;
                default:
                    throw new System.Exception("Invalid type " + model.Type);
            }

            return whereStatement;
        }
    }
}
