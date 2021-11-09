using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class PictureDao : BaseDao<IDataContext>, IDao<PictureEntity>
    {
        #region SQL-Queries
        private static readonly string SELECTALLPICTURES = "SELECT * FROM TablePictures " +
                                                            "INNER JOIN picture " +
                                                                "ON picture.id = TablePictures.picture_id";

        #endregion

        public PictureDao(IDataContext dataContext) : base(dataContext)
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
            string whereStatement = model.Type switch
            {
                "hotel" => $"WHERE type = @Type AND hotel_id = @Hotel_id",
                "room" => $"WHERE type = @Type AND room_type_id = @Room_id",
                _ => throw new System.Exception("Invalid type " + model.Type),
            };


            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<PictureEntity>($"{SELECTALLPICTURES} {whereStatement}",
                                                    new { model.Type, model.Hotel_id, model.Room_id });

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
    }
}
