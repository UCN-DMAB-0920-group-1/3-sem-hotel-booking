namespace DataAccessLayer.DTO
{
    public class RoomDto : BaseDto
    {
        public string RoomTypeHref { get; set; }
        public string Room_number { get; set; }
        public string Notes { get; set; }
        public bool? Active { get; set; }
    }
}