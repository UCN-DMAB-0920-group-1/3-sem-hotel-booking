using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class PictureDao : BaseDao<IDataContext>, IDao<PictureEntity>
    {
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
            string whereStatement = "";
            switch (model.Type)
            {

                case "hotel":
                    whereStatement = $"WHERE type = @Type AND hotel_id = @Hotel_id";
                    break;
                case "room":
                    whereStatement = $"WHERE type = @Type AND room_id = @Room_id";
                    break;
                default:
                    throw new System.Exception("Invalid type " + model.Type);    
            }


            using (IDbConnection connection = DataContext.Open())
            {
                return connection.Query<PictureEntity>($"SELECT * FROM TablePictures " +
                                                    $"INNER JOIN picture ON picture.id = TablePictures.picture_id " +
                                                    whereStatement,
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
