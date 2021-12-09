namespace Models
{
    public class RoomType : BaseModel
    {
        public string Type { get; set; }
        public int? Avaliable { get; set; }
        public int? Beds { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public int? HotelId { get; set; }
        public int? price {get; set;}
    }
}
