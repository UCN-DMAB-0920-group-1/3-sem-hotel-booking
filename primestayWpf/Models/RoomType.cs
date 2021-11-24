namespace Model
{
    public class RoomType : BaseModel
    {
        public string Type { get; set; }
        public int? Num_of_avaliable { get; set; }
        public int? Beds { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public int? HotelId { get; set; }
        public bool? Active { get; set; }
    }
}
