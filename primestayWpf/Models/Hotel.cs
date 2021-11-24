namespace Model
{
    public class Hotel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StaffedHours { get; set; }
        public int? Stars { get; set; }
        public int? LocationId { get; set; }
        public bool? Active { get; set; }


    }
}
