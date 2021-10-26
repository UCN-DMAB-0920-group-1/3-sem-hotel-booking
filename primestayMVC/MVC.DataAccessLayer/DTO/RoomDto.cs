namespace PrimeStay.MVC.DataAccessLayer.DTO
{
    public class RoomDto : BaseDto
    {
        public string Type { get; set; }
        public int? NumOfAvaliable { get; set; }
        public int? NumOfBeds { get; set; }
        public string Description { get; set; }
        public int? Rating { get; set; }
        public int? HotelId { get; set; }
    }
}
