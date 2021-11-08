namespace PrimeStayApi.Model
{
    public class RoomTypeEntity : BaseEntity
    {
        public string Type { get; set; }
        public int? beds { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public int? Hotel_Id { get; set; }
        public int? Avaliable { get; set; }
    }
}
