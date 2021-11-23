namespace Model
{
    public class Room : BaseModel
    {
        public int? RoomTypeId { get; set; }
        public string RoomNumber { get; set; }
        public string Notes { get; set; }
        public bool? Active { get; set; }
    }
}
