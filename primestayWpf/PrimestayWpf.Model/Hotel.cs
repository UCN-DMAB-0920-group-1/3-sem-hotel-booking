namespace PrimestayWpf.Model
{
    public class Hotel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string StaffedHours { get; set; }
        public int? Stars { get; set; }
        public string LocationHref { get; set; }
        public string href { get; set; }


    }
}
