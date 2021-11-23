namespace PrimestayWPF.DataAccessLayer.DTO
{
    public class RoomTypeDto : BaseDto
    {
        public string Type { get; set; }
        public int? Avaliable { get; set; }
        public int? Beds { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public string HotelHref { get; set; }
        public bool? Active { get; set; }
    }
}
