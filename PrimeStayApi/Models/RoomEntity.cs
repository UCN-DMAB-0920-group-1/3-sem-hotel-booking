namespace Models
{
    public class RoomEntity : BaseEntity
    {
        public int? Room_type_id { get; set; }
        public string Room_number { get; set; }
        public string Notes { get; set; }

        public bool? Active { get; set; }
    }
}